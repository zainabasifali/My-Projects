import java.util.ArrayList;
public class Category_List {
        ArrayList<String> categoryOfBooks = new ArrayList<>();

        void addCategoryOfBooks (String s){

            categoryOfBooks.add(s);
        }

        String printCategoryOfBooks (){
            int counter = 1;
            for (String s: categoryOfBooks) {
                System.out.println(  counter + "." +  s);
                counter ++;
            }
            return null;
        }
    }


