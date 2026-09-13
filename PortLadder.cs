using UnityEngine;

namespace BetterPorts
{
    public class PortLadder : GoPointerButton
    {
        private Collider col;
        public float upDistance = 1.25f;

        private void Awake()
        {
            col = GetComponent<Collider>();
        }

        private void Update()
        {
            col.enabled = GameState.currentBoat;
        }

        public override void OnActivate()
        {
            Transform player = GameObject.FindWithTag("PlayerController").transform;
            player.position = transform.position + Vector3.up * upDistance + transform.up * 1f;
        }
    }
}
