// �������� �.�. ��-08-4 2009 ���.


#include <iostream.h>
#include <conio.h>
#include <locale.h>
#include <stdio.h>
#define r 10

void sort1(int *a,int q) // ������� ���������� ���������
{
int i,b=0;
for(int k=0;k<q+r;k++)
{
	for(i=0;i<q;i++)
	{	
		if (a[i]<a[i+1])
			{     
			  b=a[i];
			  a[i]=a[i+1];
			  a[i+1]=b;
			}
	}
}

for(i=0;i<q;i++)
{
	printf("%i\t",a[i]);
}


}

void sort2(int *m,int q) // ������� ������������ ����������
{	
	int i=q,min,k,buf,j;
	min=i;
	for(j=i+1;j<q;j++)
	{	
		if(m[j]<m[min]) 
		{
			min=j;
			buf=m[i];
			m[i]=m[min];
			m[min]=buf;
		}

	}
	for(k=0;k<q;k++)
	{
		printf("%i\t",m[k]);
	}

}

void sort3(int *s,int q)
{	
	int i=-1,j=q,buf;
	double
	p=(s[i]+s[j])/2;// ����������� �������
do			   // ��������� ����������
{	
	while(s[i]<p) i=i+1;
	while(s[j]>p) j=j-1;
	if(i<=j)
	{
		buf=s[i];
		s[i]=s[j];
		s[j]=buf;
		i++;
		j--;	
	}

}
while(i<=j);

for(int k=0;k<q;k++)
printf("%i\t",s[k]);


}

void main()
{ 
	setlocale(LC_ALL,"rus");
	char str[50];
	printf("������������! ���� �� ������ ������������� ������ ������,������� ��������� � ��������� �����, ������� �������� ����� c �����������(.txt),�������� ��� ������ ��������� � ����� begin.txt. ���� �� ������ ������������� ���� ������ ��� ����� �������� ��������� ����, ��� �� ������ ������� �������� ����� ��������� �������, �� ������ �������� � ��������, � ���������� ���� ���� � ������� � ����������.\n\n");
	printf("������� ��� �����:\n");
	scanf("%s",str);
	printf("�������� ������:\n\n");

	FILE *begin=fopen(str,"r");//���������� ������� �� �����
	int l;
	fscanf(begin,"%d",&l);// l - ����� �������

	int *u= new int[l];

{
	int l=0;  // ������� ��� l ����������
	while (!feof(begin))
	{
		fscanf(begin,"%d",&u[l++]);
	}
}

fclose(begin);

for (int i=0;i<l;i++) 
	printf("%i\t",u[i]);// ����� ������������ �������	
	
	printf("\n\n���������� ���������:\n\n");
	sort1(u,l);
	
	printf("\n\n���������� ������� ������� ������:\n\n");
	sort2(u,l);

	printf("\n\n������� ����������:\n\n");
	sort3(u,l);
	
	getch();
}