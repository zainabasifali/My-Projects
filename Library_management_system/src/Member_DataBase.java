import java.time.Instant;
import java.util.ArrayList;

public class Member_DataBase {
    ArrayList<Member> members = new ArrayList<Member>();

    public void Adding_Members(Member member){
        members.add(member);
    }
    Member Searching_By_Id(int id){
        for(Member i:members){
            if(i.getId() == id){
                return i;
            }
        }
        System.out.println("Sorry Not Found");
        return null;
    }

    int Searching_By_Name(String name){
        for(Member i:members){
            if(i.getName().equals(name)){
                return i.getId();
            }
        }
        System.out.println("You don't have issued book");
        return 0;
    }

    public void Update(Member member,Member found){
        member.setId(found.getId());
        int index = -1;
        for(Member i:members){
            index++;
            if(i.getId() == member.getId()){
                members.set(index , member);
            }
        }
    }
    public void Delete(int id){
        Member found =  Searching_By_Id(id);
        if(found != null) {
            members.remove(found);
            System.out.println("Deleted Successfully");
        }

    }

    public void List(){
        System.out.println("MemberList");
        for(Member i:members){
            System.out.println(i.getId()+"."+i.getName());
        }
    }


}

