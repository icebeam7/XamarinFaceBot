using Microsoft.Bot.Connector.DirectLine;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XamarinFaceBot.Helpers;
using XamarinFaceBot.Modelos;

namespace XamarinFaceBot.Servicios
{
    public class ServicioBotDirectLine
    {
        public DirectLineClient ClienteDL;
        public Conversation Conversacion;
        public ChannelAccount Cuenta;

        public ServicioBotDirectLine(string nombre)
        {
            var tokenResponse = new DirectLineClient(Constantes.DirectLineSecret).Tokens.GenerateTokenForNewConversation();

            ClienteDL = new DirectLineClient(tokenResponse.Token);
            Conversacion = ClienteDL.Conversations.StartConversation();
            Cuenta = new ChannelAccount { Id = nombre, Name = nombre};
        }

        public void EnviarMensaje(string mensaje)
        {
            var activity = new Activity
            {
                From = Cuenta,
                Text = mensaje,
                Type = ActivityTypes.Message
            };

            ClienteDL.Conversations.PostActivity(Conversacion.ConversationId, activity);
        }

        public async Task ObtenerMensajes(ObservableCollection<Mensaje> collection)
        {
            string watermark = null;

            while (true)
            {
                var set = await ClienteDL.Conversations.GetActivitiesAsync(Conversacion.ConversationId, watermark);
                watermark = set?.Watermark;

                foreach (var actividad in set.Activities)
                {
                    if (actividad.From.Id == Constantes.BotID)
                        collection.Add(new Mensaje(actividad.Text, actividad.From.Name));
                }

                await Task.Delay(3000);
            }
        }
    }
}
