import java.time.LocalDate;
import java.util.*;
public class Main {
    public static void main(String[] args) {
        int choice = 0;
        int id;
        String pass;

//      Objects of Author class
        Author one = new Author("Alex Michael-ides","British–Cypriot Author",10);
        Author two = new Author("Amanda Hinnant","American Author of 21st Century",7);
        Author three = new Author("Thomas Keneally","Colonial Australia of 18 Century",9);
        Author four = new Author("Daniell Koepke","Writer, graduate student, and advocate for mental health and invisible disability",10);

//      Object of Author_DataBase class
        Author_DataBase ad = new Author_DataBase();
        ad.Add_Author(one);
        ad.Add_Author(two);
        ad.Add_Author(three);
        ad.Add_Author(four);

//      Objects of Books class
        Books One = new Novel("Novel" , "The Silent Patient",one,30);
        Books Two = new Magazine("Magazine", "Real Simple",two,80);
        Books Three = new Historical("Historical" ,"The Playmaker",three,80);
        Books Four = new Poetry("Poetry","Daring To Take Up Space",four,80);

//      Object of Books_DataBase class
        Books_DataBase B_DB = new Books_DataBase();
        B_DB.Add_Books(One);
        B_DB.Add_Books(Two);
        B_DB.Add_Books(Three);
        B_DB.Add_Books(Four);

//      Object of Member_DataBase class , Reservation_DataBase class , Category_List and Day_Reservation_DataBase
        Member_DataBase m = new Member_DataBase();
        Reservation_DataBase rd = new Reservation_DataBase();
        Category_List cl = new Category_List();
        Day_Reservation_DataBase drd = new Day_Reservation_DataBase();
        cl.addCategoryOfBooks("Novel");
        cl.addCategoryOfBooks("Magazine");
        cl.addCategoryOfBooks("Historical");
        cl.addCategoryOfBooks("Poetry");

//      Object of custom Scanner class
        scanner scan = new scanner();
        Scanner s = new Scanner(System.in);

        System.out.println("\n\n\t\t\t\t\t\t\t\t\t\t\t\tWElCOME TO THE LIBRARY MANAGEMENT SYSTEM\n\n");
        System.out.print("Enter password if you are Librarian: ");
        pass = s.nextLine();

//      if password matches system will be executed
        if(pass.equals("zbsth")) {

//      Applying while loop so that when ever user presses 7.Exit , the program gets end
            while (choice != 7) {
                System.out.println("\n\nMain Menu : \n1.Book\n2.Member\n3.Author\n4.Issue book\n5.Return Book\n6.Day Report\n7.Exit");
                System.out.print("Choose one of them : ");
                choice = scan.input_id();

//      Main switch which have 7 cases
                switch (choice) {

//              Book
                    case 1:
                        int choose = 0;
                        System.out.println("\nSub Menu : ");
                        System.out.println("1.Add Books\n2.Delete Books\n3.Search Books\n4.List of books\n5.Update Books");
                        System.out.print("\nChoose one of them : ");
                        choose = scan.input_id();

//                switch under a case of Main switch
                        switch (choose) {
//                      Book Sub Menu
                            case 1:
                                String title;
                                int noc;
                                System.out.println("Here are List of book categories available: ");
                                cl.printCategoryOfBooks();
                                System.out.print("Enter id of the category you are choosing: ");
                                int cat_id = scan.input_id();
                                if (cat_id == 1 || cat_id == 2 || cat_id == 3 || cat_id == 4) {
                                    System.out.print("Enter book title: ");
                                    title = s.next();
//                          This line is for taking input after the space as well and to take the next input
                                    title += s.nextLine();
                                    System.out.print("Enter book no of copies: ");
                                    noc = scan.input_id();
                                    System.out.println("Here is a list of Authors: ");
                                    ad.Print();
                                    System.out.print("Is the author of book inside this list ? 1.Yes 0.No: ");
                                    id = scan.input_id();
                                    if (id == 1) {
                                        System.out.print("Enter id of the author: ");
                                        id = scan.input_id();
                                        Author found = ad.Search_By_Id(id);
                                        if (found != null) {
                                            if (cat_id == 1) {
                                                B_DB.Add_Books(new Novel("Novel", title, found, noc));
                                                System.out.println("Added Successfully ! ");
                                            } else if (cat_id == 2) {
                                                B_DB.Add_Books(new Magazine("Magazine", title, found, noc));
                                                System.out.println("Added Successfully ! ");
                                            } else if (cat_id == 3) {
                                                B_DB.Add_Books(new Historical("Historical", title, found, noc));
                                                System.out.println("Added Successfully ! ");
                                            } else if (cat_id == 4) {
                                                B_DB.Add_Books(new Poetry("Poetry", title, found, noc));
                                                System.out.println("Added Successfully ! ");
                                            }

                                        } else {
                                            id = 0;
                                        }
                                    } else if (id != 0 && id != 1) {
                                        System.out.println("Wrong option chose");
                                    }
                                    if (id == 0) {
                                        System.out.print("Enter name of the author: ");
                                        String author_name = s.next();
                                        author_name += s.nextLine();
                                        System.out.print("Enter Details of author: ");
                                        String author_details = s.next();
                                        author_details += s.nextLine();
                                        System.out.print("Enter rating of the author out of 10: ");
                                        int rating = scan.input_id();
                                        ad.Add_Author(new Author(author_name, author_details, rating));
                                        ad.Print();
                                        System.out.print("Now enter id of the author: ");
                                        id = scan.input_id();
                                        Author found = ad.Search_By_Id(id);
                                        if (cat_id == 1) {
                                            B_DB.Add_Books(new Novel("Novel", title, found, noc));
                                            System.out.println("Added Successfully ! ");
                                        } else if (cat_id == 2) {
                                            B_DB.Add_Books(new Magazine("Magazine", title, found, noc));
                                            System.out.println("Added Successfully ! ");
                                        } else if (cat_id == 3) {
                                            B_DB.Add_Books(new Historical("Historical", title, found, noc));
                                            System.out.println("Added Successfully ! ");
                                        } else if (cat_id == 4) {
                                            B_DB.Add_Books(new Poetry("Poetry", title, found, noc));
                                            System.out.println("Added Successfully ! ");
                                        }
                                    }
                                } else {
                                    System.out.println("Category doesn't exist");
                                }
                                break;
                            case 2:
                                System.out.println("Here are List of book categories available: ");
                                cl.printCategoryOfBooks();
                                System.out.print("Enter id of the category you are choosing: ");
                                cat_id = scan.input_id();
                                if (cat_id == 1) {
                                    B_DB.Print_by_category("Novel");
                                    System.out.print("Enter id of the book you want to Delete: ");
                                    id = scan.input_id();
                                    B_DB.Delete(id);
                                } else if (cat_id == 2) {
                                    B_DB.Print_by_category("Magazine");
                                    System.out.print("Enter id of the book you want to Delete: ");
                                    id = scan.input_id();
                                    B_DB.Delete(id);
                                } else if (cat_id == 3) {
                                    B_DB.Print_by_category("Historical");
                                    System.out.print("Enter id of the book you want to Delete: ");
                                    id = scan.input_id();
                                    B_DB.Delete(id);
                                } else if (cat_id == 4) {
                                    B_DB.Print_by_category("Poetry");
                                    System.out.print("Enter id of the book you want to Delete: ");
                                    id = scan.input_id();
                                    B_DB.Delete(id);
                                } else {
                                    System.out.println("Category doesn't exist");
                                }
                                break;
                            case 3:
                                B_DB.Print();
                                System.out.print("Enter id of the book you want to Search: ");
                                id = scan.input_id();
                                Books found = B_DB.Search_By_Id(id);
                                if (found != null) {
                                    System.out.println("The book searched is :");
                                    System.out.println("Category of the book is: " + found.getCategory());
                                    System.out.println("Id of the book is : " + found.Get_Id());
                                    System.out.println("Title of the book is : " + found.Get_Title());
                                    System.out.println("Author of the book is : " + found.getAuthor().getName());
                                    System.out.println("No of copies of the book is : " + found.no_of_copies);
                                }
                                break;
                            case 4:
                                System.out.println("Here are List of book categories available: ");
                                cl.printCategoryOfBooks();
                                System.out.print("Enter id of the category you are choosing: ");
                                cat_id = scan.input_id();
                                if (cat_id == 1) {
                                    B_DB.Print_by_category("Novel");
                                } else if (cat_id == 2) {
                                    B_DB.Print_by_category("Magazine");
                                } else if (cat_id == 3) {
                                    B_DB.Print_by_category("Historical");
                                } else if (cat_id == 4) {
                                    B_DB.Print_by_category("Poetry");
                                } else {
                                    System.out.println("Category doesn't exist ; here are the list of all books: ");
                                    B_DB.Print();
                                }
                                break;
                            case 5:
                                B_DB.Print();
                                System.out.print("Enter id of the book you want to Update: ");
                                id = scan.input_id();
                                found = B_DB.Search_By_Id(id);
                                if (found != null) {
                                    System.out.print("What do you want to update in book 1.Title 2.Author 3.No of Copies: ");
                                    id = scan.input_id();
                                    if (id == 1) {
                                        System.out.print("Enter the new title: ");
                                        title = s.next();
                                        title += s.nextLine();
                                        if (found.getCategory().equals("Novel")) {
                                            B_DB.Update(new Novel("Novel", title, found.getAuthor(), found.getNo_of_copies()), found);
                                            rd.change_book(found, new Novel("Novel", title, found.getAuthor(), found.getNo_of_copies()));
                                            drd.change_book(found, new Novel("Novel", title, found.getAuthor(), found.getNo_of_copies()));
                                            System.out.println("Updated Successfully!");
                                        } else if (found.getCategory().equals("Magazine")) {
                                            B_DB.Update(new Magazine("Magazine", title, found.getAuthor(), found.getNo_of_copies()), found);
                                            rd.change_book(found, new Magazine("Magazine", title, found.getAuthor(), found.getNo_of_copies()));
                                            drd.change_book(found, new Magazine("Magazine", title, found.getAuthor(), found.getNo_of_copies()));
                                            System.out.println("Updated Successfully!");
                                        } else if (found.getCategory().equals("Historical")) {
                                            B_DB.Update(new Historical("Historical", title, found.getAuthor(), found.getNo_of_copies()), found);
                                            rd.change_book(found, new Historical("Historical", title, found.getAuthor(), found.getNo_of_copies()));
                                            drd.change_book(found, new Historical("Historical", title, found.getAuthor(), found.getNo_of_copies()));
                                            System.out.println("Updated Successfully!");
                                        } else if (found.getCategory().equals("Poetry")) {
                                            B_DB.Update(new Poetry("Poetry", title, found.getAuthor(), found.getNo_of_copies()), found);
                                            rd.change_book(found, new Poetry("Poetry", title, found.getAuthor(), found.getNo_of_copies()));
                                            drd.change_book(found, new Poetry("Poetry", title, found.getAuthor(), found.getNo_of_copies()));
                                            System.out.println("Updated Successfully!");
                                        }

                                    } else if (id == 2) {
                                        System.out.println("Here is a list of Authors: ");
                                        ad.Print();
                                        System.out.print("Is the author of book inside this list ? 1.Yes 0.No: ");
                                        id = scan.input_id();
                                        if (id == 1) {
                                            System.out.print("Enter id of the author: ");
                                            id = scan.input_id();
                                            Author found1 = ad.Search_By_Id(id);
                                            if (found1 != null) {
                                                if (found.getCategory().equals("Novel")) {
                                                    B_DB.Update(new Novel("Novel", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Novel("Novel", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Novel("Novel", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                } else if (found.getCategory().equals("Magazine")) {
                                                    B_DB.Update(new Magazine("Magazine", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Magazine("Magazine", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Magazine("Magazine", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                } else if (found.getCategory().equals("Historical")) {
                                                    B_DB.Update(new Historical("Historical", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Historical("Historical", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Historical("Historical", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                } else if (found.getCategory().equals("Poetry")) {
                                                    B_DB.Update(new Poetry("Poetry", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Poetry("Poetry", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Poetry("Poetry", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                }

                                            } else {
                                                id = 0;
                                            }
                                        }
                                        if (id == 0) {
                                            System.out.print("Enter name of the author: ");
                                            String author_name = s.next();
                                            author_name += s.nextLine();
                                            System.out.print("Enter Details of author: ");
                                            String author_details = s.next();
                                            author_details += s.nextLine();
                                            System.out.print("Enter rating of the author out of 10: ");
                                            int rating = scan.input_id();
                                            ad.Add_Author(new Author(author_name, author_details, rating));
                                            ad.Print();
                                            System.out.print("Now enter id of the author: ");
                                            id = scan.input_id();
                                            Author found1 = ad.Search_By_Id(id);
                                            if (found1 != null) {
                                                if (found.getCategory().equals("Novel")) {
                                                    B_DB.Update(new Novel("Novel", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Novel("Novel", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Novel("Novel", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                } else if (found.getCategory().equals("Magazine")) {
                                                    B_DB.Update(new Magazine("Magazine", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Magazine("Magazine", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Magazine("Magazine", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                } else if (found.getCategory().equals("Historical")) {
                                                    B_DB.Update(new Historical("Historical", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Historical("Historical", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Historical("Historical", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                } else if (found.getCategory().equals("Poetry")) {
                                                    B_DB.Update(new Poetry("Poetry", found.Get_Title(), found1, found.getNo_of_copies()), found);
                                                    rd.change_book(found, new Poetry("Poetry", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    drd.change_book(found, new Poetry("Poetry", found.Get_Title(), found1, found.getNo_of_copies()));
                                                    System.out.println("Updated Successfully ! ");
                                                }
                                            }
                                        }

                                    } else if (id == 3) {
                                        System.out.print("Enter the new no of copies: ");
                                        noc = scan.input_id();
                                        if (found.getCategory().equals("Novel")) {
                                            B_DB.Update(new Novel("Novel", found.Get_Title(), found.getAuthor(), noc), found);
                                            rd.change_book(found, new Novel("Novel", found.Get_Title(), found.getAuthor(), noc));
                                            drd.change_book(found, new Novel("Novel", found.Get_Title(), found.getAuthor(), noc));
                                            System.out.println("Updated Successfully!");
                                        } else if (found.getCategory().equals("Magazine")) {
                                            B_DB.Update(new Magazine("Magazine", found.Get_Title(), found.getAuthor(), noc), found);
                                            rd.change_book(found, new Magazine("Magazine", found.Get_Title(), found.getAuthor(), noc));
                                            drd.change_book(found, new Magazine("Magazine", found.Get_Title(), found.getAuthor(), noc));
                                            System.out.println("Updated Successfully!");
                                        } else if (found.getCategory().equals("Historical")) {
                                            B_DB.Update(new Historical("Historical", found.Get_Title(), found.getAuthor(), noc), found);
                                            rd.change_book(found, new Historical("Historical", found.Get_Title(), found.getAuthor(), noc));
                                            drd.change_book(found, new Historical("Historical", found.Get_Title(), found.getAuthor(), noc));
                                            System.out.println("Updated Successfully!");
                                        } else if (found.getCategory().equals("Poetry")) {
                                            B_DB.Update(new Poetry("Poetry", found.Get_Title(), found.getAuthor(), noc), found);
                                            rd.change_book(found, new Poetry("Poetry", found.Get_Title(), found.getAuthor(), noc));
                                            drd.change_book(found, new Poetry("Poetry", found.Get_Title(), found.getAuthor(), noc));
                                            System.out.println("Updated Successfully!");
                                        }
                                    } else {
                                        System.out.println("Wrong option chose");
                                    }
                                }
                                break;
                            default:
                                System.out.println("Un Available ; Kindly please choose option according to the Menu");
                        }
                        break;

//              Member
                    case 2:
                        System.out.println("\nSub Menu : ");
                        System.out.println("1.Add Member\n2.Delete Member\n3.Search Member\n4.List of Members\n5.Update Member");
                        System.out.print("\nChoose one of them : ");
                        choose = scan.input_id();

                        switch (choose) {
//                      Member Sub Menu
                            case 1:
                                System.out.print("Enter Member Name: ");
                                String name = s.next();
//                          This line is for taking input after the space as well and to take the next input
                                name += s.nextLine();
                                System.out.print("Enter Member Address: ");
                                String address = s.nextLine();
                                System.out.print("Enter Member phone number: ");
                                String phone = s.nextLine();
                                System.out.print("Is Membership done? true = Yes , false = No: ");
                                boolean ms;
                                boolean valid = false;
                                do {
                                    try {
                                        ms = s.nextBoolean();
                                        m.Adding_Members(new Member(name, address, phone, ms));
                                        System.out.println("Added Successfully ! ");
                                        System.out.println("Your Member Id is: " + m.Searching_By_Name(name));
                                        valid = true;
                                    } catch (InputMismatchException ex) {
                                        System.out.println("Your input is wrong boolean accepts true or false , kindly please enter true or false: ");
                                        valid = false;
                                        s.nextLine();
                                    }
                                }
                                while (valid != true);
                                break;
                            case 2:
                                System.out.print("Enter id of the member you want to Delete: ");
                                id = scan.input_id();
                                Reservation found = rd.Searching_By_Id(id);
                                if (found == null) {
                                    m.Delete(id);
                                } else {
                                    System.out.println("You cant delete this member , he is having reservation of the book");
                                }
                                break;
                            case 3:
                                System.out.print("Enter id of the member you want to Search: ");
                                id = scan.input_id();
                                Member found1 = m.Searching_By_Id(id);
                                if (found1 != null) {
                                    System.out.println("Searched Member is:");
                                    System.out.println("Id of the Member is : " + found1.getId());
                                    System.out.println("Name of the Member is : " + found1.getName());
                                    System.out.println("Phone number of the Member is : " + found1.getMember_Phone_number());
                                    System.out.println("Address of the Member is : " + found1.getMember_Address());
                                    System.out.println("Membership of the Member is : " + found1.isMember_ship_status());
                                }
                                break;
                            case 4:
                                System.out.println("List of members is : ");
                                m.List();
                                break;
                            case 5:
                                System.out.print("Enter id of the Member you want to Update: ");
                                id = scan.input_id();
                                found1 = m.Searching_By_Id(id);
                                if (found1 != null) {
                                    System.out.print("What do you want to update in Member 1.Name 2.Address 3.Phone number 4.Membership Status: ");
                                    id = scan.input_id();
                                    if (id == 1) {
                                        System.out.print("Enter new Name: ");
                                        name = s.nextLine();
                                        m.Update(new Member(name, found1.getMember_Address(), found1.getMember_Phone_number(), found1.isMember_ship_status()), found1);
                                        rd.change_member(found1, new Member(name, found1.getMember_Address(), found1.getMember_Phone_number(), found1.isMember_ship_status()));
                                        drd.change_member(found1, new Member(name, found1.getMember_Address(), found1.getMember_Phone_number(), found1.isMember_ship_status()));
                                        System.out.println("Updated Successfully");
                                    } else if (id == 2) {
                                        System.out.print("Enter new Address: ");
                                        address = s.nextLine();
                                        m.Update(new Member(found1.getName(), address, found1.getMember_Phone_number(), found1.isMember_ship_status()), found1);
                                        rd.change_member(found1, new Member(found1.getName(), address, found1.getMember_Phone_number(), found1.isMember_ship_status()));
                                        drd.change_member(found1, new Member(found1.getName(), address, found1.getMember_Phone_number(), found1.isMember_ship_status()));
                                        System.out.println("Updated Successfully ! ");
                                    } else if (id == 3) {
                                        System.out.print("Enter new Phone number: ");
                                        phone = s.nextLine();
                                        m.Update(new Member(found1.getName(), found1.getMember_Address(), phone, found1.isMember_ship_status()), found1);
                                        rd.change_member(found1, new Member(found1.getName(), found1.getMember_Address(), phone, found1.isMember_ship_status()));
                                        drd.change_member(found1, new Member(found1.getName(), found1.getMember_Address(), phone, found1.isMember_ship_status()));
                                        System.out.println("Updated Successfully ! ");
                                    } else if (id == 4) {
                                        System.out.print("Enter updated Membership Status: ");
                                        boolean valid1;
                                        do {
                                            try {
                                                ms = s.nextBoolean();
                                                m.Update(new Member(found1.getName(), found1.getMember_Address(), found1.getMember_Phone_number(), ms), found1);
                                                rd.change_member(found1, new Member(found1.getName(), found1.getMember_Address(), found1.getMember_Phone_number(), ms));
                                                drd.change_member(found1, new Member(found1.getName(), found1.getMember_Address(), found1.getMember_Phone_number(), ms));
                                                System.out.println("Updated Successfully ! ");
                                                valid1 = true;
                                            } catch (InputMismatchException ex) {
                                                System.out.println("Your input is wrong boolean accepts true or false , Enter again: ");
                                                valid1 = false;
                                                s.nextBoolean();
                                            }
                                        }
                                        while (valid1 != true);
                                    } else {
                                        System.out.println("Wrong option chose");
                                    }
                                }
                                break;
                            default:
                                System.out.println("Un Available ; Kindly please choose option according to the Menu");
                        }
                        break;

//              Author
                    case 3:
                        System.out.println("\nSub Menu : ");
                        System.out.println("1.Add Author\n2.Delete Author\n3.Search Author\n4.List of Authors\n5.Update Author");
                        System.out.print("\nChoose one of them : ");
                        choose = scan.input_id();
//                    Author sub menu
                        switch (choose) {
                            case 1:
                                System.out.print("Enter name of the author: ");
                                String author_name = s.next();
                                author_name += s.nextLine();
                                System.out.print("Enter Details of author: ");
                                String author_details = s.next();
                                author_details += s.nextLine();
                                System.out.print("Enter rating of the author out of 10: ");
                                int rating = scan.input_id();
                                ad.Add_Author(new Author(author_name, author_details, rating));
                                System.out.println("Added Successfully");
                                break;
                            case 2:
                                ad.Print();
                                System.out.print("Enter id of the Author you want to Delete: ");
                                id = scan.input_id();
                                ad.Delete(id);
                                break;
                            case 3:
                                ad.Print();
                                System.out.print("Enter id of the Author you want to Search: ");
                                id = scan.input_id();
                                Author found = ad.Search_By_Id(id);
                                if (found != null) {
                                    System.out.println("The Author searched is :");
                                    System.out.println("Id of the Author is : " + found.getId());
                                    System.out.println("Name of the Author is : " + found.getName());
                                    System.out.println("Author of the book details are : " + found.getQualification());
                                    System.out.println("Author rating is " + found.getRating());
                                }
                                break;
                            case 4:
                                System.out.println("List of Authors: ");
                                ad.Print();
                                break;
                            case 5:
                                ad.Print();
                                System.out.print("Enter id of the Author you want to Update: ");
                                id = scan.input_id();
                                found = ad.Search_By_Id(id);
                                if (found != null) {
                                    System.out.print("What do you want to update in Author 1.Name 2.Details 3.Rating: ");
                                    id = scan.input_id();
                                    if (id == 1) {
                                        System.out.print("Enter name of the author: ");
                                        author_name = s.next();
                                        author_name += s.nextLine();
                                        ad.Update(new Author(author_name, found.getQualification(), found.getRating()), found);
                                        B_DB.change_author_details(found, new Author(author_name, found.getQualification(), found.getRating()));
                                        rd.change_author(found, new Author(author_name, found.getQualification(), found.getRating()));
                                        drd.change_author(found, new Author(author_name, found.getQualification(), found.getRating()));
                                        System.out.println("Updated Successfully");
                                    } else if (id == 2) {
                                        System.out.print("Enter details of the author: ");
                                        author_details = s.next();
                                        author_details += s.nextLine();
                                        ad.Update(new Author(found.getName(), author_details, found.getRating()), found);
                                        B_DB.change_author_details(found, new Author(found.getName(), author_details, found.getRating()));
                                        rd.change_author(found, new Author(found.getName(), author_details, found.getRating()));
                                        drd.change_author(found, new Author(found.getName(), author_details, found.getRating()));
                                        System.out.println("Updated Successfully");
                                    } else if (id == 3) {
                                        System.out.print("Enter rating of the author out of 10: ");
                                        rating = scan.input_id();
                                        ad.Update(new Author(found.getName(), found.getQualification(), rating), found);
                                        B_DB.change_author_details(found, new Author(found.getName(), found.getQualification(), rating));
                                        rd.change_author(found, new Author(found.getName(), found.getQualification(), rating));
                                        System.out.println("Updated Successfully");
                                    } else System.out.println("Wrong option chose");
                                }
                                break;
                            default:
                                System.out.println("Un Available ; Kindly please choose option according to the Menu");
                        }
                        break;

//              Issue Book
                    case 4:
                        Member found = null;
                        int allow = 0;
                        LocalDate today = LocalDate.now();
                        System.out.print("Are you a Member already ? 1.Yes 0.No: ");
                        id = scan.input_id();
                        if (id == 1) {
                            System.out.print("Enter Member Id ?: ");
                            int member_id = scan.input_id();
                            found = m.Searching_By_Id(member_id);
                            if (found == null) {
                                System.out.println("You are not a member already kindly register your self first");
                                id = 0;
                            } else if (found != null) {
                                if (found.isMember_ship_status() == true) {
                                    allow = rd.check_already_exist(found.getId());
                                    if (allow == 0) {
                                        System.out.println("Here are List of book categories available: ");
                                        cl.printCategoryOfBooks();
                                        System.out.print("Enter id of the category you are choosing: ");
                                        int cat_id = scan.input_id();
                                        if (cat_id == 1) {
                                            B_DB.Print_by_category("Novel");
                                        } else if (cat_id == 2) {
                                            B_DB.Print_by_category("Magazine");
                                        } else if (cat_id == 3) {
                                            B_DB.Print_by_category("Historical");
                                        } else if (cat_id == 4) {
                                            B_DB.Print_by_category("Poetry");
                                        } else {
                                            System.out.println("Category doesn't exist ; here are all the books available in library");
                                            B_DB.Print();
                                        }
                                        System.out.print("Enter the id of the book you want to choose: ");
                                        id = scan.input_id();
                                        Books found_book = B_DB.Search_By_Id(id);
                                        if (found_book != null) {
                                            rd.Adding_Members_Reserving_books(new Reservation(found_book, found, today.plusDays(0), today.plusDays(10)));
                                            drd.Adding_Members_Reserving_books(new Reservation(found_book, found, today.plusDays(0), today.plusDays(10)));
                                            System.out.println("Issued Successfully");
                                        }
                                    } else if (allow == 1) {
                                        System.out.println("You have already issued one book");
                                    }
                                } else {
                                    System.out.println("You are not a member of library : hence pay 1000 Rs and get your librarian updated.");
                                }
                            }

                        }
                        if (id == 0) {
                            System.out.print("Enter Member Name: ");
                            String name = s.next();
                            name += s.nextLine();
                            System.out.print("Enter Member Address: ");
                            String address = s.nextLine();
                            System.out.print("Enter Member phone number: ");
                            String phone = s.nextLine();
                            System.out.print("Is Membership done? write true if Yes , write False if No: ");
                            boolean ms;
                            boolean valid = false;
                            do {
                                try {
                                    ms = s.nextBoolean();
                                    m.Adding_Members(new Member(name, address, phone, ms));
                                    System.out.println("Added Successfully ! ");
                                    System.out.println("Your Member Id is: " + m.Searching_By_Name(name));
                                    valid = true;
                                } catch (InputMismatchException ex) {
                                    System.out.println("Your input is wrong boolean accepts true or false , kindly please enter true or false: ");
                                    valid = false;
                                    s.nextLine();
                                }
                            }
                            while (valid != true);
                            System.out.print("Enter your Member id issuing the book ?: ");
                            int member_id = scan.input_id();
                            found = m.Searching_By_Id(member_id);

                            if (found != null) {
                                if (found.isMember_ship_status() == true) {
                                    allow = rd.check_already_exist(found.getId());
                                    if (allow == 0) {
                                        System.out.println("Here are List of book categories available: ");
                                        cl.printCategoryOfBooks();
                                        System.out.print("Enter id of the category you are choosing: ");
                                        int cat_id = scan.input_id();
                                        if (cat_id == 1) {
                                            B_DB.Print_by_category("Novel");
                                        } else if (cat_id == 2) {
                                            B_DB.Print_by_category("Magazine");
                                        } else if (cat_id == 3) {
                                            B_DB.Print_by_category("Historical");
                                        } else if (cat_id == 4) {
                                            B_DB.Print_by_category("Poetry");
                                        } else {
                                            System.out.println("Category doesn't exist ; here are all the books available in library");
                                            B_DB.Print();
                                        }
                                        System.out.print("Enter the id of the book you want to choose: ");
                                        id = scan.input_id();
                                        Books found_book = B_DB.Search_By_Id(id);
                                        if (found_book != null) {
                                            rd.Adding_Members_Reserving_books(new Reservation(found_book, found, today.plusDays(0), today.plusDays(10)));
                                            drd.Adding_Members_Reserving_books(new Reservation(found_book, found, today.plusDays(0), today.plusDays(10)));
                                            System.out.println("Issued Successfully");
                                        }
                                    } else if (allow == 1) {
                                        System.out.println("You have already issued one book");
                                    }
                                } else {
                                    System.out.println("You are not a member of library : hence pay 1000 Rs and get your librarian updated.");
                                }
                            }
                        }
                        break;

//              Return Book
                    case 5:
                        System.out.print("Enter Member id: ");
                        id = scan.input_id();
                        Reservation found_reserve = rd.Searching_By_Id(id);
                        if (found_reserve != null) {
                            System.out.println(rd.Calculate_Fine(found_reserve));
                            rd.Delete(id);
                        } else {
                            System.out.println("This Id doesn't exist");
                        }
                        break;

//              Day Report
                    case 6:
                        System.out.println("Day Report : ");
                        drd.Print_Reservations_of_the_day();
                        break;

//              Exit
                    case 7:
                        System.exit(0);
                        drd.Clear();
                    default:
                        System.out.println("Un Available ; Kindly please choose option according to the Menu");
                }

            }
        }
        else{
            System.out.println("Password not matched : Authentication failed");
        }
    }
}
