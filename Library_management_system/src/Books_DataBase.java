import java.awt.print.Book;
import java.util.ArrayList;

public class Books_DataBase {
    ArrayList<Books> B = new ArrayList<Books>();

    void Add_Books(Books book){
        B.add(book);
    }

    Books Search_By_Id(int id){
        for(Books i:B){
            if(i.Get_Id() == id ){
                return i;
            }
        }
        System.out.println("Sorry Book Not Found");
        return null;
    }

    void Print_by_category(String category){
        for(Books i:B){
            if(i.getCategory().equals(category)){
                System.out.println(i.Get_Id()+"."+i.Get_Title());
            }
        }
    }

   /* As author is having has-a relationship with book so if author is changed so book written by
    that author should change author detailLs*/
    public void change_author_details(Author o , Author n_o){
        n_o.setId(o.getId());
        for(Books i:B){
            if(i.getAuthor().getId() == o.getId()){
                i.setAuthor(n_o);
            }
        }
    }

    public void Print(){
        System.out.println("Book List :");
        for(Books i:B) {
            System.out.println(i.Get_Id() + " = " + i.Get_Title());
        }
    }
    public void Delete(int id){
        Books found =  Search_By_Id(id);
        if(found != null) {
            B.remove(found);
            System.out.println("Deleted Successfully");
        }
    }
    public void subtract_no_of_copies(Books o){
        if(o.no_of_copies > 0) {
            o.setNo_of_copies(o.getNo_of_copies() - 1);
        }
        else{
            System.out.println("This book is currently not available");
        }
    }
    public void add_no_of_copies(Books o){
        o.setNo_of_copies(o.getNo_of_copies()+1);
    }
    public void Update(Books book,Books found){
        book.setId(found.Get_Id());
        int index = -1;
        for(Books i:B){
            index++;
            if(i.Get_Id() == book.Get_Id()){
                B.set(index , book);
            }
        }
    }



}
