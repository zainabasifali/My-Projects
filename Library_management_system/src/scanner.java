import java.util.InputMismatchException;
import java.util.Scanner;

public class scanner {

    Scanner s = new Scanner(System.in);

    public int input_id() {
        boolean valid = true;
        do {
            try {
                int id = s.nextInt();
                valid = true;
                return id;
            } catch (InputMismatchException ex) {
                System.out.println("Wrong Input,Enter Integer");
                s.nextLine();
                valid = false;
            }
        }
        while (valid != true);
        return 0;
    }

}
