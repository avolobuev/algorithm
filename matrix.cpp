#include <iostream>
#include <fstream>
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <math.h>

using namespace std;

void RussianMessage(char *message)
{
	char rmessage[256];
	CharToOem(message,rmessage);
	cout<<rmessage;
}

int RussianMenu()
{
	RussianMessage("\n������� 1 ��� ���������� ������\n");
	RussianMessage("������� 2 ��� ������ ������\n");
	RussianMessage("������� 3 ��� �������� ��� ��������� ������\n");
	RussianMessage("������� 4 ��� ��������� ������� �� �����\n");
	RussianMessage("������� 5 ��� ������������ ������\n");
	RussianMessage("������� 6 ��� ���������� ������������ �������\n");
	RussianMessage("������� 7 ��� ������� ������� ������\n");
	RussianMessage("������� 8 ��� ������� ������� �������\n");
	RussianMessage("������� 9 ��� ���������� �� �����\n");
	RussianMessage("������� 10 ��� ������ � ����\n");
	RussianMessage("������� 11 ��� ������� ������\n");
	RussianMessage("������� 12 ��� ������\n");
	int choice;
	cin>>choice;
	return choice;
}
class Matrix
{	

public:

	int Column;
	int Row;
	double **m;

	Matrix()
	{
		Column=0;
		Row=0;
		m=NULL;
	}

	Matrix(Matrix & copy) // ����������� �����������
	{  
		create(copy.Row,copy.Column);
		for (int i=0;i<copy.Row;i++)
			for (int j=0;j<copy.Column;j++)
				m[i][j]=copy.m[i][j];
	}

	void create(int maxRow,int maxColumn) // ����������� ��������  ������
	{
		Column=maxColumn;
		Row=maxRow;

		m=new double*[maxRow];
		for (int i=0;i<maxRow;i++) 
		m[i]=new double[maxColumn];
	}
	
	
	void clear() // ������� �������� ������
	{
		if (m!=NULL)
		{
			for (int i=0;i<Row;i++)
			{
				delete[]m[i];
			}

		delete []m;
		m=NULL;
	}
	Column=0;
	Row=0;
}

~Matrix() // ����������
{
	clear();
}

Matrix operator=(Matrix& A)
{
	for (int i=0;i<A.Row;i++)
	{
		for (int j=0;j<A.Column;j++)
		{
			m[i][j]=A(i,j);
		}
	}
	return *this;
}

double *operator[](int i);
bool operator==(Matrix& A);
double &Matrix::operator()(int i, int j);

};


double* Matrix::operator[](int i)
{
	return m[i];
}

bool Matrix::operator==(Matrix& A)
{
	for (int i=0; i<A.Row; i++)
	{
		for (int j=0; j<A.Column; j++)
		{
			if(m[i][j]!=A.m[i][j])
				return false;
		}
	}
	return true;
}

double &Matrix::operator()(int i, int j)
{
	return m[i][j];
}
	
Matrix operator+(Matrix &A,Matrix &B)
{
	Matrix C;

	if ((A.Column==B.Column)&&(A.Row==B.Row))
	{
		C.create(A.Row,A.Column);

		for (int i=0;i<A.Row;i++)
			for (int j=0;j<A.Column;j++)
				C(i,j)=A(i,j)+B(i,j);
	}
	else RussianMessage("������!!! ������������ ���-�� �����  ��� ��������...");

	return C;
}

Matrix operator-(Matrix &A,Matrix &B)
{
	Matrix C;

	if ((A.Column==B.Column)&&(A.Row==B.Row))
	{
		C.create(A.Row,A.Column);

		for (int i=0;i<A.Row;i++)
			for (int j=0;j<B.Column;j++)
				C(i,j)=A(i,j)-B(i,j);
	}
	else RussianMessage("������!!! ������������ ���-�� �����  ��� ��������...");

	return C;
}

Matrix operator*(double a,Matrix &A)
{
	Matrix C;

		C.create(A.Row,A.Column);

		for (int i=0;i<A.Row;i++)
			for (int j=0;j<A.Column;j++)
				C(i,j)=(A(i,j))*a;
	return C;
}

Matrix operator*(Matrix &A,Matrix &B)
{
	Matrix C;

	if (A.Column==B.Row)
	{
		C.create(A.Row,B.Column);
		for (int i=0;i<A.Row;i++) 
			for (int j=0;j<B.Column;j++)
			{
				C(i,j)=0;
				for(int k=0;k<A.Column;k++)
				{
					C(i,j)=C(i,j)+(A(i,k))*(B(k,j));
				}
			}
	}
	else RussianMessage("������!!! ������������ ���-�� �����  ��� ��������...");

	return C;
}


void input(Matrix &A,int maxRaw,int maxColumn)// ������� ���������� �������
{
	for (int i=0;i<maxRaw;i++)
		{
			cout <<"\n";
			for (int j=0;j<maxColumn;j++)
			{
				RussianMessage("������� �[");
				cout<<i+1;
				RussianMessage("][");
				cout<<j+1;
				RussianMessage("]="); 
				cin>>A.m[i][j]; 
			} 
	}
}

double det(Matrix &A)
{
	double m;
	if (A.Column==A.Row)
	{

	double cstr=1;
	int i,j,i1;
	double c;

	i=0;


	for(i=0;i<A.Column;i++)
	{
		int k=0;
		int minus=-1;
	while (A(i,i)==0) 
	{

		k++;
		if ((i+k)==A.Column) 
		{
			cstr=0;
			break;
		}
		else 
		{
			cstr*=minus;
		for (j=0;j<A.Column;j++)
		{
			c=A(i,j);
			A(i,j)=A(i+k,j);
			A(i+k,j)=c;
		}
		}

	}
		c=A(i,i);

	for (j=0;j<A.Column;j++)
	{
		A(i,j)=(A(i,j))/c;
	}

	cstr*=c;

	i1=i+1;
	while(i1<A.Column)
	{
		m=A(i1,i);

		for (j=0;j<A.Column;j++)
		{
			A(i1,j)=A(i1,j)-m*A(i,j);
		}
			i1++;

	}
	k=i;
	}
	return cstr;

	}
	else 
	{
		RussianMessage("������!!! ������� �� �����������...");
		return 0;
	}


}

void solve(Matrix &A,Matrix B)
{
	Matrix R;
	if (A.Row==B.Row)
	{
		double m;
		double *b;
		b=new double[A.Row];
	if (A.Column==A.Row)
	{
		double cstr = 1;
		int i,k,j,i1;
		double c;
		R.create(A.Row,A.Column+1);
		for(i=0;i<A.Row;i++)
			for(j=0;j<A.Column+1;j++)
			{
				R(i,j)=A(i,j);
				if (A.Column==j) R(i,j)=B(i,0);
			}

		i=0;


	for (i=0;i<A.Column;i++) 
	{
		k=i;
		int t=0;
		int minus=-1;
	while (R(i,i)==0) 
	{

		t++;
		if((i+t)==A.Column) 
		{
			cstr=0;
			break;
		}
		else 
		{
			cstr*=minus;
			for (j=0;j<A.Column+1;j++)
			{
				c=R(i,j);
				R(i,j)=R(i+t,j);
				R(i+t,j)=c;
			}
		}

	}
		c=R(i,i);
	
	for(j=0;j<A.Column+1;j++)
	{
		R(i,j)=R(i,j)/c;
	}

	cstr=cstr*c;

	i1=i+1;
	while(i1<A.Column)
	{
		m=R(i1,i);

		for (j=0; j<A.Column+1;j++)
		{
			R(i1,j)=R(i1,j)-m*R(i,j);
		}

		i1++;

	}
	k=i;
	}

	c=0;
	b[k]=R(k,k+1);

for (i=k-1;i>=0;i--)
{
	c=R(i,k+1);
	j=i+1;

	while (j<=k)
	{
		c=c-R(i,j)*b[j];
		j++;

	}		

b[i]=c;

}
cout<<"\n";
for (i=0;i<A.Row;i++)
cout<<"x"<<i<<"="<<b[i]<<"\n";


	}   
	else 
		{ 
			RussianMessage("������!!! ������� �� �����������...");
		}


	}
	else 
		{
			RussianMessage("������!!!"); 
		}
}

istream& operator >> (istream& is, Matrix& O)
{
	int m,n;
	is>>m>>n;
	O.create(m,n);
	for (int i=0;i<m;i++)
	{
		for ( int j=0;j<n;j++)
			is>>O(i,j);
	}
	return is;
}

ostream& operator << (ostream& os,Matrix& O)
{	
	for (int i=0;i<O.Row;i++)
	{	
		for ( int j = 0; j < O.Column; j++)
			os<<O(i,j)<<" ";
			os<<"\n";
	}


	return os;
}


void Kramer(Matrix& A,Matrix B)
{
	int i,j;
	double d=det(A);
	int n=B.Row;
	Matrix T,X;
	T.create(n,n);	
	X.create(n,1);
	T=A;
	RussianMessage("������� ������� �������:\n");
	for(i=0;i<n;i++)
	{	
	
		for(j=0;j<n;j++)			
		
			T[j][i]=B[j][0];
		
		X[i][1]=(det(T))/d;

	cout<<"x"<<i<<"="<<X[i][1]<<"\n";
	}	
}


void main()
{

	Matrix A,B,X;
	int n,m,p,t=0;
	char znak;
		
do
	{
		t=RussianMenu();
		switch(t)
		{
			case 1: 
				RussianMessage("������� ����� �������:");
				scanf("%i",&p);
				
				if(p==1) 
				{       
					RussianMessage("������� ����������� m x n:");
					scanf("%i x %i",&m,&n);
					A.create(m,n);
					input(A,m,n);
					system("cls");
				}	
				else if (p == 2)
						{
							RussianMessage("������� ����������� m x n:");
							scanf("%i x %i",&m,&n);
							B.create(m,n);
							input(B,m,n);
							system("cls");
						}
							else 
							{		
								system("cls");
								RussianMessage("������� 1 ��� 2");
							}
				
				break;

			case 2:
				RussianMessage("������� ����� �������:");
				scanf("%i",&p);
				//system("cls");
				
				cout<<"\n";

				if (p==1)
						cout<<"A:\n"<<A; 
					else 
						if (p==2)
							cout<<"B:\n"<<B;
									else 
									{
										RussianMessage("������� 1 ��� 2");
									}
				
				break;

			case 3:
				//system("cls");
				cout<<"\n";
				RussianMessage("������� + ��� -:");
				scanf("%s",&znak);
				if(znak=='+')
				{
					cout<<"A+B:\n"<<(A+B);
				}
				else if(znak=='-')
					{
						cout << "1. A-B \n2. B-A\n";
						scanf("%i",&p);
						//system("cls");
						cout<<"\n";
						if ( p == 1 ) cout<<"A-B:\n"<<(A-B); else if (p == 2) cout<<"B-A:\n"<<(B-A);else RussianMessage("������� 1 ��� 2");

					}
				else RussianMessage("������� + ��� -:");
				break;

				
			case 4:
				RussianMessage("������� ����� �� ������� �������� �������:");
				cin>>p;
				RussianMessage("������� ����� �������:");
				cin>>t;
				//system("cls");
				cout<<"\n";
				if (t == 1) cout<<"n*A:\n"<<(p*A); else if (t == 2) cout<<"n*B:\n"<<(p*B); else RussianMessage("������� 1 ��� 2");
				break;
			case 5:
				RussianMessage("�������:\n1. A*B \n2. B*A\n\n");
				scanf("%i",&p);
				//system("cls");
				if (p==1) cout<<"A*B:\n"<<(A*B); else if (p==2) cout<<"B*A:\n"<<(B*A);else RussianMessage("������� 1 ��� 2");		
				break;
			case 6:
				RussianMessage("������� ��� ����� ������� ������� ������������:\n1.A \n2.B\n");
				scanf("%i",&p);
				//system("cls");
				cout<<"\n";
				if(p==1) 
					cout<<"det:\n"<<det(A)<<"\n";
				else 
					if(p==2) 
						cout<<"det:\n"<<det(B)<<"\n";
					else RussianMessage("������� 1 ��� 2");		
				break;
			case 7:
				RussianMessage("������� ����� �������:");
				cin>>p;
				RussianMessage("������� ������ ��������� ������:");     
				if (p==1)
				{
					X.create(A.Row,1);
					input(X,A.Row,1);//system("cls");
					cout<<"\n";
					solve(A,X);
				}
				else if (p==2)
				{
					X.create(A.Row,1);
					input(X,B.Row,1);//system("cls");
					cout<<"\n";
					solve(B,X);
				}
				
				else RussianMessage("������� 1 ��� 2");		
				break;
			
			case 8:
				RussianMessage("������� ����� �������:");
				cin>>p;
				RussianMessage("������� ������ ��������� ������:");     
				if (p==1)
				{
					X.create(A.Row,1);
					input(X,A.Row,1);//system("cls");
					cout<<"\n";
					Kramer(A,X);
				}
				else if (p==2)
				{
					X.create(A.Row,1);
					input(X,B.Row,1);//system("cls");
					cout<<"\n";
					Kramer(B,X);
				}
				
				else RussianMessage("������� 1 ��� 2");		
				break;

			case 9: 
				RussianMessage("������� ����� �������:");
				char c[255];
				cin>>p;
				RussianMessage("������� ��� �����:");
				cin>>c;
				if (p==1)
				{
					ifstream a(c);
					if (a.is_open())
					a>>A;
				}
				else if (p==2)
				{		
					ifstream a(c);
					if (a.is_open())
						a>>B;
				}
				
				else RussianMessage("������� 1 ��� 2");
				system("cls");
				break;
		
			case 10: 
				RussianMessage("������� ����� �������:");
				cin >> p;
				RussianMessage("������� ��� �����:");
				cin>>c;
				if (p==1)
				{
					ofstream a(c);
					a<<A;
				}
				else if (p==2)
				{
					ofstream a(c);
					a<<B;
				}
				
				else RussianMessage("������� 1 ��� 2");
				system("cls");
				break;

			case 11:
				RussianMessage("������� ����� �������:");
				cin>>p;
				if(p==1)
				{
					A.clear();
				}
				else if(p==2)				
				{
					B.clear();
				}
				
				else RussianMessage("������� 1 ��� 2");
				system("cls");
				break;
			
			case 12: 
				return;
				//break;
			default: 
				RussianMessage("�������� ����\n");
		}
	}while (t!=12);
		

	return;
}