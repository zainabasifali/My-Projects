import java.util.ArrayList;

public class Author_DataBase {
    ArrayList<Author> a = new ArrayList<Author>();

    public void Add_Author(Author author){
        a.add(author);
    }

    Author Search_By_Id(int id){
        for(Author i:a){
            if(i.getId() == id ){
                return i;
            }
        }
        System.out.println("Sorry Not Found");
        return null;
    }
    public void Print(){
        System.out.println("Author List :");
        for(Author i:a) {
            System.out.println(i.getId() + " = " + i.getName());
        }
    }
    public void Delete(int id){
        Author found =  Search_By_Id(id);
        if(found != null) {
            a.remove(found);
            System.out.println("Deleted Successfully ! ");
        }
    }

    public void Update(Author author,Author found){
        author.setId(found.getId());
        int index = -1;
        for(Author i:a){
            index++;
            if(i.getId() == author.getId()){
                a.set(index , author);
            }
        }
    }

}
