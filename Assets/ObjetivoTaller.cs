using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjetivoTaller : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor[] socketsRequeridos;

    [Header("Textos del Panel")]
    [SerializeField] private TextMeshProUGUI textoDescripcion;
    [SerializeField] private TextMeshProUGUI textoMision;

    [Header("Mensajes al Completar")]
    [SerializeField] private string descripcionCompletada = "Has equipado al maniquí y ordenado la forja del Jarl.";
    [SerializeField] private string misionCompletada = "¡ARSENAL LISTO PARA LA BATALLA!";
    [SerializeField] private Color colorCompletado = new Color(0.1f, 0.55f, 0.15f);

    private string descripcionInicial;
    private string misionInicial;
    private Color colorDescripcionInicial;
    private Color colorMisionInicial;
    private bool objetivoCumplido;

    private void Start()
    {
        if (textoDescripcion != null)
        {
            descripcionInicial = textoDescripcion.text;
            colorDescripcionInicial = textoDescripcion.color;
        }

        if (textoMision != null)
        {
            misionInicial = textoMision.text;
            colorMisionInicial = textoMision.color;
        }
    }

    private void Update()
    {
        if (socketsRequeridos == null || socketsRequeridos.Length == 0)
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

            if (textoDescripcion != null)
            {
                textoDescripcion.text = descripcionCompletada;
                textoDescripcion.color = colorCompletado;
            }

            if (textoMision != null)
            {
                textoMision.text = misionCompletada;
                textoMision.color = colorCompletado;
            }
        }
        else if (!todosOcupados && objetivoCumplido)
        {
            objetivoCumplido = false;

            if (textoDescripcion != null)
            {
                textoDescripcion.text = descripcionInicial;
                textoDescripcion.color = colorDescripcionInicial;
            }

            if (textoMision != null)
            {
                textoMision.text = misionInicial;
                textoMision.color = colorMisionInicial;
            }
        }
    }
}