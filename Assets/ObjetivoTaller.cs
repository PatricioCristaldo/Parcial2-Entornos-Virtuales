using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjetivoTaller : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor[] socketsRequeridos;
    [SerializeField] private TextMeshProUGUI textoPanel;

    [Header("Mensajes")]
    [SerializeField] private string textoInicial = "Bienvenido al Taller 3D";
    [SerializeField] private string textoCompletado = "Felicidades ordenaste el Taller 3D";

    private bool objetivoCumplido;

    private void Start()
    {
        if (textoPanel != null)
        {
            textoPanel.text = textoInicial;
            textoPanel.color = Color.white;
        }
    }

    private void Update()
    {
        if (socketsRequeridos == null || socketsRequeridos.Length == 0 || textoPanel == null)
            return;

        bool todosOcupados = true;

        for (int i = 0; i < socketsRequeridos.Length; i++)
        {
            if (socketsRequeridos[i] == null || !socketsRequeridos[i].hasSelection)
            {
                todosOcupados = false;
                break;
            }
        }

        if (todosOcupados && !objetivoCumplido)
        {
            objetivoCumplido = true;
            textoPanel.text = textoCompletado;
            textoPanel.color = Color.green;
        }
        else if (!todosOcupados && objetivoCumplido)
        {
            objetivoCumplido = false;
            textoPanel.text = textoInicial;
            textoPanel.color = Color.white;
        }
    }
}