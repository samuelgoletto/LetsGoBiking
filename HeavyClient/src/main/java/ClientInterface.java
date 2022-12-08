import com.soap.ws.client.generated.IGoBikeService;

import java.util.Scanner;

public class ClientInterface {

    static void execute(IGoBikeService iGoBikeService) {
        Scanner scanner = new Scanner(System.in);
        String startAddress, destinationAddress;
        boolean firstTurn = true;
        do {
            if (!firstTurn) {
                System.out.println("Address error");
            }

            System.out.print("Start address : ");
            startAddress = scanner.nextLine();
            System.out.print("Destination address : ");
            destinationAddress = scanner.nextLine();
        } while (startAddress.isEmpty() || destinationAddress.isEmpty());

        String queueName = iGoBikeService.getItinary(startAddress, destinationAddress);
        System.out.println("Listening to queue '" + queueName + "' :");

        ActiveMqClient a = new ActiveMqClient();
        a.configurer(queueName);
    }
}
