using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class MousePositionIndicator : MonoBehaviour {
        public float interactRadius;
        [SerializeField] List<Entity> entitiesInRadius;
        [SerializeField] Entity currentSelectedEntity;

        Camera cam;

        void Start() {
            cam = Camera.main;
            // Create collision;
            CircleCollider2D col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = interactRadius;
            col.isTrigger = true;
        }

        void Update() {
            transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
            Entity closestEntity = null;
            float closestDistance = float.PositiveInfinity;

            foreach (var entity in entitiesInRadius) {
                var distance = Vector3.Distance(entity.transform.position, transform.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestEntity = entity;
                }
            }

            if (currentSelectedEntity != closestEntity) {
                if (currentSelectedEntity) {
                    currentSelectedEntity.Release();
                }

                currentSelectedEntity = closestEntity;

                if (currentSelectedEntity) {
                    currentSelectedEntity.Lock();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            var entity = other.GetComponentInParent<Entity>();

            if (entity != null && other.name.ToLower().Equals("body")) {
                entitiesInRadius.Add(entity);
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            var entity = other.GetComponentInParent<Entity>();

            if (entity != null && other.name.ToLower().Equals("body")) {
                entitiesInRadius.Remove(entity);
            }

            if (entitiesInRadius.Count == 0 && currentSelectedEntity != null) {
                currentSelectedEntity.Release();
                currentSelectedEntity = null;
            }
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
    }
}
