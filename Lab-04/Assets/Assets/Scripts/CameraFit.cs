using UnityEngine;

// Ajusta o tamanho da câmera para que ela nunca enxergue mais largura do que
// o fundo tem (10.24 unidades). Sem isso, em telas widescreen (16:9) apareceria
// uma faixa vazia na lateral, porque o parallax usa só 2 sprites lado a lado.
// Coloque este script na Main Camera.
[RequireComponent(typeof(Camera))]
public class CameraFit : MonoBehaviour
{
    public float backgroundWidth = 10.24f;   // 1024 px / 100 pixels por unidade
    public float backgroundHeight = 7.68f;   //  768 px / 100 pixels por unidade

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
    }

    void Update()
    {
        // orthographicSize = METADE da altura visível. A largura visível é size * 2 * aspect.
        float sizePelaLargura = (backgroundWidth / 2f) / cam.aspect;
        float sizePelaAltura = backgroundHeight / 2f;

        // Escolhe o menor, para nunca ultrapassar as bordas do fundo.
        cam.orthographicSize = Mathf.Min(sizePelaLargura, sizePelaAltura);
    }
}
