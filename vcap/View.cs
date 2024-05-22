using System.Drawing.Imaging;
using AForge.Video;

namespace vcap
{
    
    public partial class View : Form
    {
        // Declara um campo readonly para o controlador de c�mera
        private readonly ICameraController controller;

        // Declara um campo readonly para o caminho onde as imagens s�o guardadas
        private readonly string path = @"C:\uab\LDS\Atividades\efolioC\vcapMVC\imagens\";

        // Construtor da classe View.
        public View()
        {
            InitializeComponent(); // Inicia os componentes do interface gr�fico
            ICameraModel model = new CameraModel(); // Inicia o modelo de c�mera
            controller = new CameraController(model); // Inicia o controlador de c�mera com o modelo
            CarregaCameras(); // Carrega as c�meras dispon�veis na interface
        }

        // M�todo para carregar os nomes das c�meras dispon�veis no comboBox.
        private void CarregaCameras()
        {
            comboBox1.Items.AddRange(controller.GetCameraNames()); // Adiciona os nomes das c�meras ao comboBox.
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0; // Seleciona o primeiro item, se houver c�meras dispon�veis.
        }

        // Evento disparado ao executar o bot�o de iniciar a c�mera.
        private void Button1_Click(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex; // Obt�m o �ndice da c�mera selecionada no comboBox.
            try
            {
                controller.StartCamera(i, CapturaCam); // Inicia a captura de v�deo da c�mera selecionada.
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Erro ao iniciar a c�mera", MessageBoxButtons.OK, MessageBoxIcon.Error); // Exibe uma mensagem de erro se ocorrer uma exce��o.
            }
        }

        // Manipulador de eventos para capturar um novo frame da c�mera.
        private void CapturaCam(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagem = (Bitmap)eventArgs.Frame.Clone(); // Copia o frame atual.
            pictureBox1.Image = Imagem; // Exibe a imagem capturada no pictureBox1.
        }

        // Evento disparado ao executar o bot�o de parar a c�mera.
        private void button5_Click(object sender, EventArgs e)
        {
            controller.StopCamera(); // Parar a captura de v�deo.
        }

        // Evento disparado ao executar o bot�o de salvar a imagem.
        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox2.Image = pictureBox1.Image; // Copia a imagem do pictureBox1 para o pictureBox2.
                string filePath = System.IO.Path.Combine(path, "frame.jpg"); // Define o caminho para guardar a imagem.
                pictureBox2.Image.Save(filePath, ImageFormat.Jpeg); // Guarda a imagem como JPEG.
            }
        }

        // M�todo para abrir uma pasta e selecionar uma imagem.
        private void OpenFolderAndSelectImage()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Selecione a pasta contendo a imagem";
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    using (OpenFileDialog fileDialog = new OpenFileDialog())
                    {
                        fileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                        fileDialog.Title = "Selecione a imagem";
                        fileDialog.InitialDirectory = folderDialog.SelectedPath;

                        DialogResult fileResult = fileDialog.ShowDialog();

                        if (fileResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fileDialog.FileName))
                        {
                            pictureBox2.Image = Image.FromFile(fileDialog.FileName); // Exibe a imagem selecionada no pictureBox2.
                        }
                    }
                }
            }
        }

        // Evento disparado ao executar o bot�o de abrir a pasta e selecionar uma imagem.
        private void buttonOpenFolderAndSelectImage_Click(object sender, EventArgs e)
        {
            OpenFolderAndSelectImage(); // Chama o m�todo para abrir a pasta e selecionar uma imagem.
        }

        // Evento disparado ao executar o bot�o de abrir a pasta e selecionar uma imagem.
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFolderAndSelectImage(); // Chama o m�todo para abrir a pasta e selecionar uma imagem.
        }

        // Evento disparado ao executar o bot�o de aplicar o filtro grayscale.
        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap filteredImage = controller.ApplyGrayscaleFilter((Bitmap)pictureBox2.Image.Clone()); // Aplica o filtro grayscale.
                pictureBox2.Image = filteredImage; // Exibe a imagem filtrada no pictureBox2.

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Imagem JPEG|*.jpg";
                    saveDialog.Title = "Salvar imagem como...";
                    saveDialog.FileName = "imagem_filtrada";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveDialog.FileName;
                        filteredImage.Save(savePath, ImageFormat.Jpeg); // Guarda a imagem filtrada.
                    }
                }
            }
            else
            {
                // Exibe mensagem de erro se n�o houver imagem.
                MessageBox.Show("N�o h� imagem para aplicar o filtro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento disparado ao executar o bot�o de sair.
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Encerra a aplica��o.
        }

        // Evento disparado quando o formul�rio � fechado.
        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.StopCamera(); // Parar a captura de v�deo ao fechar o formul�rio.
        }
    }
}