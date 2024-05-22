
Programa de Captura e Processamento de Vídeo com AForge.NET
O programa foi desenvolvido utilizando a API AForge.NET (API AForge.NET - https://www.aforgenet.com/framework/), uma biblioteca para processamento de imagem e vídeo em C#. 
O objetivo é permitir a captura de vídeo a partir de uma camera, além das funcionalidades para processar e guardar os frames/imagens capturados.

Descrição:
O programa detecta automaticamente as câmeras disponíveis no sistema e permite que o utilizador escolha uma camera para utilizar na captura de vídeo.
Após selecionar a câmera desejada, o utilizador pode iniciar a captura de vídeo clicando no botão video. 
Os frames capturados são exibidos em tempo real numa PictureBox no interface(view) do programa.
O utilizador pode capturar um frame específico clicando no botão capturar.
O frame/imagem capturado é então guardado numa pasta especificada no programa - imagens.
O programa permite a funcionalidade de aplicar um filtro de escala de cinzentos aos frames capturados.
O filtro é aplicado a partir de um clique do botão Filtro P/B e o resultado é exibido na segunda PictureBox no interface do programa.
O utilizador pode guardar a imagem processada em formato JPEG numa pasta do sistema.
O utilizador pode abrir uma imagem existente no sistema para exibição na segunda PictureBox, o que permite a possibilidade de aplicar filtros a imagens já existentes. 
Botão sair opção de saída que desliga a câmera (se estiver em execução) e fecha o programa.

Como Utilizar:
Selecionar Câmera: Escolha a câmera desejada a partir da caixa de escolha.
Capturar Vídeo: clicar no botão "Video" para começar a captura de vídeo.
Gravação de Frames: clicar no botão "Capturar" para salvar um frame específico capturado pela câmera.
Processamento de Imagem: clicar no botão "Filtro / Guardar" para aplicar um filtro de escala de cinzentos ao frame/imagem atual e guardar a imagem processada.
Abrir uma Imagem Existente: clicar no botão "Abrir" para abrir uma imagem existente no sistema para visualização e se quiser processamento da imagem.
Encerramento, clicar no botão "Sair" para encerrar a aplicação.

Requisitos de Sistema:
S.O. Windows e .NET Framework instalado

Observações:
Certificar de que todas as permissões estão concedidas para o acesso à câmera e à pasta para guardar as imagens.
Problemas de execução, verificar que a câmera está corretamente ligada ao sistema e se os drivers estão atualizados.

Este programa utiliza a API AForge.NET, para as funcionalidades de processamento de imagem e vídeo.

README é apenas uma orientação básica para o uso do programa. Consulte a documentação do AForge.NET para obter informações mais detalhadas sobre as funcionalidades e implementações específicas.

Este programa está a ser desenvolvido na UC-Laboratório de Desenvolvimento de Software.
Aluno: 1902333 - Sergio Esteves - equipa 14

