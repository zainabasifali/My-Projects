import java.time.LocalDate;
import java.util.ArrayList;

public class Reservation_DataBase {
    ArrayList<Reservation> r = new ArrayList<>();
    Books_DataBase bd = new Books_DataBase();
    public void Adding_Members_Reserving_books(Reservation reserve) {
        bd.subtract_no_of_copies(reserve.getB());
        r.add(reserve);
    }

    Reservation Searching_By_Id(int id){
        for(Reservation i:r){
            if(i.getM().getId() == id){
                return i;
            }
        }
        System.out.println("Sorry Not Found");
        return null;
    }
    public int check_already_exist(int id){
        for(Reservation i:r){
            if(i.getM().getId() == id){
                return 1;
            }
        }
        return 0;
    }
    public void Delete(int id){
        Reservation found =  Searching_By_Id(id);
        bd.add_no_of_copies(found.getB());
        if(found != null) {
            r.remove(found);
        }
        else
        {System.out.println("ID not Found");}

    }
    public String Calculate_Fine(Reservation o) {
        LocalDate today = LocalDate.now();
        if((today.isBefore(o.getReturn_date())) || today.isEqual(o.getReturn_date())){
            return "No Fine , Thank you";
        }
        return "You are late , give rs " + Reservation.fine + "to the librarian";
    }

    /* As Reservation is having has-a relationship with book so if book details are changed so the reserved book details
     should be changed*/
    public void change_book(Books o , Books n_o){
        n_o.setId(o.Get_Id());
        for(Reservation i:r){
            if(i.getB().Get_Id() == o.Get_Id()){
                i.setB(n_o);
            }
        }
    }
    /* As Reservation is having has-a relationship with book and book with author so if author details are changed
     so the reserved book author details should be changed*/
    public void change_author(Author o , Author n_o){
        n_o.setId(o.getId());
        for(Reservation i:r){
            if(i.getB().getAuthor().getId() == o.getId()){
                i.getB().setAuthor(n_o);
            }
        }
    }
    /* As Reservation is having has-a relationship with Member so if Member details are changed so the
    reserved book Member details should be changed*/
    public void change_member(Member o , Member n_o){
        n_o.setId(o.getId());
        for(Reservation i:r){
            if(i.getM().getId() == o.getId()){
                i.setM(n_o);
            }
        }
    }
}
