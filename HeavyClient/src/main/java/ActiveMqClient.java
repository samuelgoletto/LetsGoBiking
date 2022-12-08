import javax.jms.*;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import java.util.Hashtable;

public class ActiveMqClient implements javax.jms.MessageListener {
    private javax.jms.Connection connect = null;
    private javax.jms.Session sendSession = null;
    private javax.jms.Session receiveSession = null;
    private javax.jms.MessageProducer sender = null;
    private javax.jms.Queue queue = null;
    InitialContext context = null;

    public void configurer(String queueName) {
        try
        {
            Hashtable properties = new Hashtable();
            properties.put(Context.INITIAL_CONTEXT_FACTORY,
                    "org.apache.activemq.jndi.ActiveMQInitialContextFactory");
            properties.put(Context.PROVIDER_URL, "tcp://localhost:61616");
            context = new InitialContext(properties);
            javax.jms.ConnectionFactory factory = (ConnectionFactory) context.lookup("ConnectionFactory");
            connect = factory.createConnection();
            this.configurerConsommateur(queueName);
            connect.start();
        } catch (javax.jms.JMSException jmse){
            jmse.printStackTrace();
        } catch (NamingException e) {
            e.printStackTrace();
        }
    }

    private void configurerConsommateur(String queueName) throws JMSException, NamingException{
        receiveSession = connect.createSession(false,javax.jms.Session.AUTO_ACKNOWLEDGE);
        Queue queue = (Queue) context.lookup("dynamicQueues/" + queueName);
        javax.jms.MessageConsumer qReceiver = receiveSession.createConsumer(queue);
        qReceiver.setMessageListener(this);
    }

    @Override
    public void onMessage(Message message) {
        TextMessage m = (TextMessage) message;
        try {
            System.out.println(m.getText());
        } catch (JMSException e) {
            e.printStackTrace();
        }
    }
}
