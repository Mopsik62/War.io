using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War.io.Enemy;


namespace War.io
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pointer;

        public RectTransform canvasRect;

        public float edgeOffset = 5f; 


        [SerializeField]
        private UnityEngine.Camera _cam;

        [SerializeField]
        private GameManager _gameManager;

        public Dictionary<EnemyCharacter, GameObject> EnemyToPointer { get; private set; } = new();

        private Vector3 fromPlayerToEnemy;

        private void Start()
        {
            _cam = UnityEngine.Camera.main;
        }
        public void SetPointers(EnemyCharacter enemy)
        {
            if (!EnemyToPointer.ContainsKey(enemy))
            {
                GameObject newPointer = Instantiate(_pointer, transform);
                EnemyToPointer.Add(enemy, newPointer);
            }
        }
        public void RemoveEnemy(EnemyCharacter enemy)
        {
            if (EnemyToPointer.ContainsKey(enemy))
            {
                if (EnemyToPointer[enemy] != null)
                {
                    Destroy(EnemyToPointer[enemy]);
                }

                EnemyToPointer.Remove(enemy);
            }
        }

        protected void Update()
        {
            foreach (var pair in EnemyToPointer)
            {
                if (pair.Key == null) continue;
                fromPlayerToEnemy = pair.Key.transform.position - _gameManager.Player.transform.position;
                //fromPlayerToEnemy.y += 1f;
                Ray ray = new Ray(_gameManager.Player.transform.position, fromPlayerToEnemy);
                Debug.DrawRay(_gameManager.Player.transform.position, fromPlayerToEnemy);

                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_cam);

                float minDistance = Mathf.Infinity;

                for (int i = 0; i < 4; i++)
                {
                    if (planes[i].Raycast(ray, out float distance))
                    {
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                        }
                    }
                }

                Vector3 worldPosition = ray.GetPoint(minDistance);

                float angle = Mathf.Atan2(worldPosition.x, worldPosition.z) * Mathf.Rad2Deg;

                Debug.Log(angle);

                //pair.Value.transform.position = worldPosition;
                MovePointerToCanvasEdge(pair.Value, angle);
            }
        }

        void MovePointerToCanvasEdge(GameObject pointer1, float angle)
        {
            if (canvasRect == null) return;

            var pointer = pointer1.GetComponent<RectTransform>();

            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;
            angle = Mathf.Repeat(angle, 360f);
            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Sin(angleRad), Mathf.Cos(angleRad));
            Vector2 edgePosition = GetCanvasEdgePosition(direction, canvasWidth, canvasHeight);

            pointer.anchoredPosition = edgePosition - direction * edgeOffset;
        }

        Vector2 GetCanvasEdgePosition(Vector2 direction, float canvasWidth, float canvasHeight)
        {
            Vector2 center = Vector2.zero;
            float maxDistance = Mathf.Max(canvasWidth, canvasHeight) * 2f;

            for (float distance = 0; distance < maxDistance; distance += 1f)
            {
                Vector2 testPoint = center + direction * distance;

                if (Mathf.Abs(testPoint.x) >= canvasWidth * 0.5f ||
                    Mathf.Abs(testPoint.y) >= canvasHeight * 0.5f)
                {
                    return new Vector2(
                        Mathf.Clamp(testPoint.x, -canvasWidth * 0.5f, canvasWidth * 0.5f),
                        Mathf.Clamp(testPoint.y, -canvasHeight * 0.5f, canvasHeight * 0.5f)
                    );
                }
            }
            return center;
        }


    }
}