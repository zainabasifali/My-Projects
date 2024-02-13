import java.util.ArrayList;

public class Day_Reservation_DataBase {

   /* We created two databases of reservation because if a member is reserving a book and then
    returning it so the particular member should be deleted from the reservation list but the day reservation
    should not delete it for having a day record*/
    ArrayList<Reservation> r_day = new ArrayList<>();

    public void Adding_Members_Reserving_books(Reservation reserve) {
        r_day.add(reserve);
    }
    public void Print_Reservations_of_the_day(){
        for(Reservation i:r_day){
            System.out.println("Id of the member is: " + i.getM().getId());
            System.out.println("Name of the member is: " + i.getM().getName());
            System.out.println("Name of the book is: " + i.getB().Get_Title());
            System.out.println("Author of the book is: " + i.getB().getAuthor().getName());
            System.out.println("No of copies of the book "+ i.getB().Get_Title()+ " left is: " + i.getB().no_of_copies);
            System.out.println("Issue date of the book is: " + i.getIssue_date());
            System.out.println("Return date of the book is: " + i.getReturn_date());
        }
    }

    /* As Reservation is having has-a relationship with book so if book details are changed so the reserved book details
     should be changed*/
    public void change_book(Books o , Books n_o){
        n_o.setId(o.Get_Id());
        for(Reservation i:r_day){
            if(i.getB().Get_Id() == o.Get_Id()){
                i.setB(n_o);
            }
        }
    }
    /* As Reservation is having has-a relationship with book and book with author so if author details are changed
     so the reserved book author details should be changed*/
    public void change_author(Author o , Author n_o){
        n_o.setId(o.getId());
        for(Reservation i:r_day){
            if(i.getB().getAuthor().getId() == o.getId()){
                i.getB().setAuthor(n_o);
            }
        }
    }
    /* As Reservation is having has-a relationship with Member so if Member details are changed so the
    reserved book Member details should be changed*/
    public void change_member(Member o , Member n_o){
        n_o.setId(o.getId());
        for(Reservation i:r_day){
            if(i.getM().getId() == o.getId()){
                i.setM(n_o);
            }
        }
    }

    public void Clear(){
        r_day.clear();
    }

}
