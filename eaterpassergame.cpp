#include <stdio.h>
#include <stdlib.h>

// printing of  board
void show(int row,int col, char a[row][col]){
for(int i=0;i<row;i++){
        printf("\n\t       |");
    for(int j=0;j<col;j++){
        printf(" %c = ",a[i][j]);
        printf("  (%d,%d)    |",i+1,j+1);
    }
        if(i == (row-1))
            {
                printf("\n\t");
            }
            else
            {
                printf("\n\t\t");
            }
}
}

// Winning conditions
void win(int row ,int col,int pr,int pc ,char a[row][col]){
   int fall;
   int i,j;
   
    // check  col
  for(i = 0; i < row; i++){
            if(a[i][pc] != 'P'){
            fall =0;
                break;}
            if(a[i][pc] == 'P'){
               fall = 1;
            }
        }


      if(fall == 1){
                printf("\nCongratulations Passer Win !\n ");
                 show(row,col,a);
                 printf("\nGame over\n");
                exit(0);
            }


    //   check diagnol
    for(i=0 ; i<row ; i++){
           if(a[i][i]=='P') {
            fall =1;
           }
           else{
            fall = 0;
            break;
           }
            }
    
    if(fall == 1){
                printf("\nCongratulations Passer Win !\n ");
                 show(row,col,a);
                 printf("\nGame over\n");
                exit(0);
            }
    
    // check anti diagnol
    for(i=0 ; i<row ; i++){
        if(a[i][(row-1)-i] == 'P'){
            fall = 1;
    }
    else{
        fall = 0;
        break;
    }
        }
        if(fall == 1){
                printf("\nCongratulations Passer Win !\n ");
                show(row,col,a);
                printf("\nGame over\n");
                exit(0);
        }
        }

// checking passer conditions
void checkp(int pr , int pc , int row , int col,char a[row][col]){
 if(pr>=row || pc>=col || a[pr][pc] != 0){
        printf("\nCheck again you are out of range or some one is already there!");
        printf("\n\nRe passer turn:");
        printf("\nEnter the row no:");
        scanf("%d",&pr);
        printf("Enter the col no:");
        scanf("%d",&pc);
        pr = pr - 1;
        pc = pc - 1;
        checkp(pr,pc,row,col,a);


 }


 else{
    a[pr][pc] = 'P';
    win(row,col,pr,pc,a);
 }
}

// checking random conditions
void checkr(int rr , int rc , int row , int col,char a[row][col]){
 if(rr>=row || rc>=col){  // || a[rr][rc] != 0
        printf("\nCheck again you are out of range or some one is already there!");
        printf("\n\nRe random turn:");
        printf("\nEnter the row no:");
         rr = rand() % row + 1;
        printf("%d",rr);
        printf("\nEnter the col no:");
        rc = rand() % col + 1;
        printf("%d",rc);
        rr = rr - 1;
        rc = rc - 1;
       checkr(rr,rc,row,col,a);


 }


 else{
    a[rr][rc] = 'R';
 }
}

// checking eater conditions
void checke(int er , int ec , int row , int col , char a[row][col]){
 if(er>=row || ec>=col){
        printf("\nCheck again you are out of range!");
        printf("\n\nRe eater turn:");
        printf("\nEnter the row no:");
        scanf("%d",&er);
        printf("Enter the col no:");
        scanf("%d",&ec);
        er = er - 1;
        ec = ec - 1;
checke(er,ec,row,col,a);
 }
 else{
     a[er][ec]='E';
 }


}

// passer turn
void passer_turn(int row,int col,char a[row][col]){
    int pr,pc;
        printf("\nPassers turn:");
        printf("\nEnter the row no:");
        scanf("%d",&pr);
        printf("Enter the col no:");
        scanf("%d",&pc);
        pr = pr - 1;
        pc = pc - 1;
            
         checkp(pr,pc,row,col,a);
}

// eater turn
void eater_turn(int row,int col,char a[row][col]){
    int er,ec;
        printf("\nEater turn:");
        printf("\nEnter the row no:");
        scanf("%d",&er);
        printf("Enter the col no:");
        scanf("%d",&ec);
        er = er - 1;
        ec = ec - 1;
             checke(er,ec,row,col,a);
             show(row,col,a);
}

// random turn
void random_turn(int row,int col,char a[row][col]){
    int rr,rc;
    printf("\nRandom turn:");
    printf("\nEnter the row no:");
    rr = rand() % row + 1;
    printf("%d\n",rr);
    printf("Enter the col no:");
    rc = rand() % col + 1;
    printf("%d\n",rc);
        rr = rr - 1;
        rc = rc - 1;
    checkr(rr,rc,row,col,a);
    show(row,col,a);
    
}

int main()
{
    int option;
    printf("Choose any one of them : \n 1: for Eater Passer game \n 2: for Eater/Passer and random player game \n");
    printf("Write your option here : ");
    scanf("%d",&option);
    
    int row,col;
    printf("\nEnter row size of board : ");
    scanf("%d",&row);
    printf("Enter col size of board : ");
    scanf("%d",&col);
    
    char a[row][col];
    
    if(row>2 && col>2 && row == col && row<=15 && col<=15){
    char turn = 'P';
    
    printf("\nGet Ready Game Is Going To Start!\n");
    
    // keeping all elements to zero 
    for(int i=0;i<row;i++){
    for(int j=0;j<col;j++){
        a[i][j]=0;
    }}
    
    show(row,col,a);
    
    // Taking inputs according to the options
    switch(option){
    case 1:
    for(int i=0;i<row;i++){
    for(int j=0;j<col;j++){
    
       while(a[i][j] == 0){
        if(turn == 'P'){
         passer_turn(row,col,a);
         turn = 'E';
        }
        
        else if(turn == 'E'){ 
         eater_turn(row,col,a);
         turn = 'P';
        }
       }  
    }
}
printf("\n\nFinal board\n\n");
 show(row,col,a);
 break;
 
case 2:
turn = 'P';
    for(int i=0;i<row;i++){
    for(int j=0;j<col;j++){
    
      while(a[i][j] == 0){
        if(turn == 'P'){
         passer_turn(row,col,a);
         turn = 'R';
        }
        
        else if(turn == 'R'){ 
         random_turn(row,col,a);
         turn = 'P';
        }
      }  
    }
}
printf("\n\nFinal board\n\n");
 show(row,col,a);
 printf("\nGame over\n");
 break;


default:
printf("\nEnter appropiate option!\n");
break;
 
}
}


else{
    printf("\nPlease enter row and col equal and greater than two and less than 15\n");
}
    return 0;
}



