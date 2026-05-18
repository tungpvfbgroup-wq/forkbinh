using BillGameCore.Modules.Input.Commands;
using BillGameCore.SharedPorts.Input;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using EntityId = BillGameCore.Core.ValueObjects.EntityId;

namespace BillGameCore.Modules.Input.Infrastructure
{
    // Bộ chuyển đổi MonoBehaviour: biến đầu vào thô của Unity thành các đối tượng lệnh (command).
    // R16: Đây là lớp (class) DUY NHẤT được phép đẩy các lệnh vào hàng đợi (enqueue commands).
    public sealed class InputReader : MonoBehaviour
    {
#if UNITY_EDITOR
        // Lưới bảo hiểm chỉ chạy trong Editor dành cho màn chơi gốc (baseline scene) đã được phê duyệt.
        // Khi game chạy thực tế (Runtime), quyền sở hữu vẫn thuộc về trường dữ liệu _actions được tuần tự hóa; 
        // Khi có nhiều màn chơi hoặc nhiều file cấu hình nút bấm khác nhau, hãy tự tay kéo gán trực tiếp trường _actions cho từng màn chơi.
        private const string DefaultActionsAssetPath = "Assets/Settings/GameInput.inputactions";
#endif

        [Inject] private CommandBuffer _buffer;

        [SerializeField] private InputActionAsset _actions;

        private EntityId _controlledEntityId = EntityId.Invalid;
        private InputContext _currentContext = InputContext.Player;
        private InputActionGateway _gateway;

        private bool _attackHeld;
        private float _attackHeldStart;

        public void SetControlledEntity(EntityId id)
        {
            if (!id.IsValid)
                throw new ArgumentException("Controlled entity id must be valid.", nameof(id));

            _controlledEntityId = id;
        }

        public void ValidateConfiguration()
        {
            TryAssignDefaultActionsAssetInEditor();
            using (new InputActionGateway(_actions)) { }
        }

        public void SwitchContext(InputContext context)
        {
            if (_buffer == null)
                throw new InvalidOperationException($"{nameof(InputReader)} requires {nameof(CommandBuffer)} injection before switching input context.");

            _attackHeld = false;
            _buffer.Clear();
            EnsureGateway();
            _gateway.SwitchContext(context);
            _currentContext = context;
        }

        private void Awake()
        {
            TryAssignDefaultActionsAssetInEditor();
            EnsureGateway();
            _gateway.SwitchContext(_currentContext);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            TryAssignDefaultActionsAssetInEditor();
        }

        private void OnValidate()
        {
            TryAssignDefaultActionsAssetInEditor();
        }
#endif

        private void Update()
        {
            if (!_controlledEntityId.IsValid) return;

            EnsureGateway();
            switch (_currentContext)
            {
                case InputContext.Player:
                    ReadPlayerMap();
                    break;
                case InputContext.Vehicle:
                    ReadVehicleMap();
                    break;
            }
        }

        private void OnDestroy()
        {
            _gateway?.Dispose();
            _gateway = null;
        }

        private void ReadPlayerMap()
        {
            var now = Time.time;
            var move = _gateway.ReadPlayerMove();
            var isMoving = move.sqrMagnitude > 0.0001f;

            _buffer.Enqueue(new MoveCommand(_controlledEntityId, move.x, move.y, isMoving, now));

            if (_gateway.WasPlayerAttackPressedThisFrame())
            {
                _attackHeld = true;
                _attackHeldStart = now;
            }

            if (_attackHeld)
            {
                _buffer.Enqueue(new AttackCommand(
                    _controlledEntityId,
                    isHeld: true,
                    heldDuration: now - _attackHeldStart,
                    timestamp: now));
            }

            if (_gateway.WasPlayerAttackReleasedThisFrame())
                _attackHeld = false;

            if (_gateway.WasPlayerInteractPressedThisFrame())
                _buffer.Enqueue(new InteractCommand(_controlledEntityId, now));
        }

        private void ReadVehicleMap()
        {
            // Các lệnh điều khiển xe (Vehicle commands) sẽ được thêm vào khi lát cắt tính năng Xe (Vehicle slice) được tạo ra.
        }

        private void EnsureGateway()
        {
            if (_actions == null)
                throw new InvalidOperationException($"{nameof(InputReader)} requires an InputActionAsset.");

            _gateway ??= new InputActionGateway(_actions);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void TryAssignDefaultActionsAssetInEditor()
        {
#if UNITY_EDITOR
            if (_actions != null)
                return;

            // Đừng coi việc này giống như mô hình Service Locator khi game đang chạy (Runtime).
            // Nó chỉ giúp ngăn chế độ Play Mode trong Editor bị lỗi sau khi màn chơi (scene) hoặc mã nguồn (script) bị làm mới (refresh).
            _actions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(DefaultActionsAssetPath);
            if (_actions != null && !UnityEngine.Application.isPlaying)
                EditorUtility.SetDirty(this);
#endif
        }
    }
}
