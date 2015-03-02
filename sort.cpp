// Волобуев А.Н. АС-08-4 2009 год.


#include <iostream.h>
#include <conio.h>
#include <locale.h>
#include <stdio.h>
#define r 10

void sort1(int *a,int q) // функция сортировки пузырьком
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

void sort2(int *m,int q) // функция классической сортировки
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
	p=(s[i]+s[j])/2;// центральный элемент
do			   // процедура разделения
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
	printf("Здравствуйте! Если вы хотите отсортировать массив данных,который находится в текстовом файле, введите название файла c расширением(.txt),например мой массив находится в файле begin.txt. Если вы хотите отсортировать свой массив для этого создайте текстовой файл, где на первой строчке напишити число элементов массива, на второй элементы с пробелом, и скопируйте этот файл в каталог с программой.\n\n");
	printf("Введите имя файла:\n");
	scanf("%s",str);
	printf("Исходный массив:\n\n");

	FILE *begin=fopen(str,"r");//считывание массива из файла
	int l;
	fscanf(begin,"%d",&l);// l - длина массива

	int *u= new int[l];

{
	int l=0;  // граница для l считывания
	while (!feof(begin))
	{
		fscanf(begin,"%d",&u[l++]);
	}
}

fclose(begin);

for (int i=0;i<l;i++) 
	printf("%i\t",u[i]);// вывод прочитанного массива	
	
	printf("\n\nСортировка пузырьком:\n\n");
	sort1(u,l);
	
	printf("\n\nСортировка методом прямого выбора:\n\n");
	sort2(u,l);

	printf("\n\nБыстрая сортировка:\n\n");
	sort3(u,l);
	
	getch();
}