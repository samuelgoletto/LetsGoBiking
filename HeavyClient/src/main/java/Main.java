import com.soap.ws.client.generated.GoBikeService;
import com.soap.ws.client.generated.IGoBikeService;

import javax.jms.JMSException;

public class Main {
    public static void main(String[] args) throws JMSException {
        GoBikeService goBikeService = new GoBikeService();
        IGoBikeService iGoBikeService = goBikeService.getBasicHttpBindingIGoBikeService();

        ClientInterface.execute(iGoBikeService);
    }
}
