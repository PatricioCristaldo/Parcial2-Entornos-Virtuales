using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ReiniciarTaller : MonoBehaviour
{
    [SerializeField] private Transform[] piezas;

    private Vector3[] posicionesIniciales;
    private Quaternion[] rotacionesIniciales;

    private void Start()
    {
        posicionesIniciales = new Vector3[piezas.Length];
        rotacionesIniciales = new Quaternion[piezas.Length];

        for (int i = 0; i < piezas.Length; i++)
        {
            if (piezas[i] != null)
            {
                posicionesIniciales[i] = piezas[i].position;
                rotacionesIniciales[i] = piezas[i].rotation;
            }
        }
    }

    public void ReiniciarPiezas()
    {
        StartCoroutine(ReiniciarConPausaSockets());
    }

    private IEnumerator ReiniciarConPausaSockets()
    {
        XRSocketInteractor[] sockets = FindObjectsByType<XRSocketInteractor>(FindObjectsSortMode.None);

        // 1. Apagar temporalmente los sockets para que suelten y no vuelvan a atraer las piezas
        foreach (var socket in sockets)
        {
            socket.socketActive = false;
        }

        // 2. Soltar de manos/sockets y devolver cada pieza a la mesa
        for (int i = 0; i < piezas.Length; i++)
        {
            if (piezas[i] == null) continue;

            if (piezas[i].TryGetComponent<XRGrabInteractable>(out var grab) && grab.isSelected)
            {
                grab.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grab);
            }

            if (piezas[i].TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            piezas[i].SetPositionAndRotation(posicionesIniciales[i], rotacionesIniciales[i]);
        }

        // 3. Sincronizar colisiones antes de reactivar los sockets
        Physics.SyncTransforms();
        yield return new WaitForSeconds(0.15f);

        foreach (var socket in sockets)
        {
            socket.socketActive = true;
        }
    }
}