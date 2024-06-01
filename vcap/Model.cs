using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Timers;

namespace vcap
{
    // Implementar a interface ICameraModel para fornecer uma classe que gere câmeras e processamento de imagens.
    public class CameraModel : ICameraModel
    {
        private VideoCaptureDevice? CameraEscolhida; // Dispositivo de captura de vídeo selecionado
        private readonly Grayscale grayscaleFilter; // Filtro de escala de cinza
        private readonly System.Timers.Timer captureTimer; // Temporizador para captura de imagens em intervalos
        private readonly string captureDirectory = @"C:\uab\LDS\Atividades\efolioC\vcap\imagens\auto\"; // Diretório para salvar as imagens capturadas

        public FilterInfoCollection Cameras { get; private set; } // Informações sobre dispositivos de câmera disponíveis

        public CameraModel()
        {
            Cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice); // Inicializar a coleção de câmeras disponíveis
            grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721); // Inicializar o filtro de escala de cinza valores de luminancia
            captureTimer = new System.Timers.Timer(20000); // Inicializar o temporizador com um intervalo de 20 segundos
            captureTimer.Elapsed += CaptureTimerElapsed; // Adicionar um manipulador de eventos para o evento Elapsed do temporizador

            // Verificar se a pasta de captura existe e, se não exitir, criar
            if (!Directory.Exists(captureDirectory))
            {
                Directory.CreateDirectory(captureDirectory);
            }
        }

        // Obtém os nomes dos dispositivos de câmera disponíveis
        public string[] GetCameraNames()
        {
            string[] cameraNomes = new string[Cameras.Count];
            for (int i = 0; i < Cameras.Count; i++)
            {
                cameraNomes[i] = Cameras[i].Name.ToString();
            }
            return cameraNomes;
        }

        // Iniciar a câmera especificada pelo índice e associar um manipulador de eventos para a captura de novos frames
        public void StartCamera(int index, NewFrameEventHandler captureHandler)
        {
            if (Cameras.Count > 0)
            {
                ExitCam(); // Certificar que não há câmeras em execução antes de iniciar uma nova
                string NomeVideo = Cameras[index].MonikerString; // Obter o moniker string do dispositivo de vídeo selecionado
                CameraEscolhida = new VideoCaptureDevice(NomeVideo); // Inicializar um novo dispositivo de captura de vídeo
                CameraEscolhida.NewFrame += captureHandler; // Associar o manipulador de eventos para o evento NewFrame
                CameraEscolhida.Start(); // Iniciar a captura de vídeo
                captureTimer.Start(); // Iniciar o temporizador de captura periódica de imagens
            }
        }

        // Interromper a captura de vídeo e parar o temporizador de captura
        public void StopCamera()
        {
            captureTimer.Stop(); // Interromper o temporizador de captura
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.SignalToStop(); // Interromper a captura de vídeo
            }
        }

        // Certificar que a câmera está parada antes de encerrá-la
        private void ExitCam()
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.SignalToStop(); // Interromper a captura de vídeo
                CameraEscolhida = null;
            }
        }

        // Manipulador de eventos para o temporizador de captura
        private void CaptureTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (CameraEscolhida != null && CameraEscolhida.IsRunning)
            {
                CameraEscolhida.NewFrame -= SaveFrameHandler; // Remover qualquer manipulador de eventos existente
                CameraEscolhida.NewFrame += SaveFrameHandler; // Adicionar um novo manipulador de eventos para capturar um frame
            }
        }

        // Manipulador de eventos para capturar e guardar um frame de vídeo
        private void SaveFrameHandler(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone(); 
            SaveFrame(frame); // Salva o quadro capturado
            CameraEscolhida.NewFrame -= SaveFrameHandler; // Remover o manipulador de eventos para evitar múltiplas capturas do mesmo frame
        }

        // Guardar um frame de imagem na pasta especificada
        private void SaveFrame(Bitmap frame)
        {
            string fileName = Path.Combine(captureDirectory, $"frame_{DateTime.Now:yyyyMMdd_HHmmss}.jpg"); // Gerar um nome para o ficheiro único baseado na data e hora
            frame.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg); // Guardar o frame/imagem como um arquivo JPEG
            frame.Dispose(); // Libertar os recursos do frame/imagem
        }

        // Aplicar o filtro de escala de cinza a uma imagem especificada
        public Bitmap ApplyGrayscaleFilter(Bitmap image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "A imagem fornecida não pode ser nula."); // Verifica se a imagem fornecida é nula
            }

            return grayscaleFilter.Apply(image); // Aplicar o filtro de escala de cinza à imagem
        }

    }
}
