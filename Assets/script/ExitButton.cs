using UnityEngine;
using UnityEditor;
public class ExitButton : MonoBehaviour
{
    // Этот метод нужно привязать к кнопке через инспектор
    public void ExitGame()
    {
        // Закрывает приложение в собранной игре
        Application.Quit();

        // Если вы тестируете в редакторе, строчка ниже просто покажет сообщение в консоли,
        // потому что Application.Quit() не работает в редакторе.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}