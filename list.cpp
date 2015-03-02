// Волобуев3.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"


void RussianMessage(char *message)//функция русификации
{
	char rmessage[256];
	CharToOem(message,rmessage);
	cout<<rmessage;
}

int RussianMenu()//функция выбора данных
{
	RussianMessage("\nВведите 1 для добавления новой записи в файл\n");
	RussianMessage("Введите 2 для показа всех записей\n");
	RussianMessage("Введите 3 для удаления всех записей\n");
	RussianMessage("Введите 4 для удаления одного элемента\n");
	RussianMessage("Введите 5 для поиска по ключу\n");
	RussianMessage("Введите 6 для поиска по индексу\n");
	RussianMessage("Введите 7 для записи данных в файл\n");
	RussianMessage("Введите 8 для считывания из файла\n");
	RussianMessage("Введите 9 для выхода\n");
	int choice;
	cin>>choice;
	return choice;
}
class List // класс моего односвязного списка
{
	private:
		struct data
		{
			int nomer;
			string name;
			int age;
			data *next;
		};
		data *head;
	public:
		List()// конструктор
		{
			head=new data;
			head->next=NULL; 
		}                     
		~List()// деструктор
		{
			delete head;
		}
		void create();
		void delList();
		void print();
		void searchONindex();
		void searchONkey();
		void add();
		void del();
		void output(data *buf);
		void SaveToFile();// функция сохранения в файл файла
		void TakeFromFile();
};

void List::create()
{
	data *buf;
	string name;
	buf=head;
	int kp=0,age;
		
	kp++;
	RussianMessage("Введите Фамилию:");
	cin >> name;
	RussianMessage("\nВведите возраст:");
	cin >> age;

	buf->next = new data;
	buf=buf->next;
	buf->nomer=kp;
	buf->name=name;
	buf->age=age;
	buf->next=NULL;
	
}

void List::print()
{
	data *buf;
	buf=head->next;
	RussianMessage("\n\t\tСписок:\n");
	while (buf!=NULL)
	{
		List::output(buf);
		buf=buf->next;// переходим на следующий элемент
	}
	cout<<endl;
}

void List::delList()
{
	data *buf3,*buf4;
	buf3=head;
	buf4=buf3->next;
	while(buf4!=NULL)// пока есть элементы удаляем их по одному
	{	
		buf3=buf4;
		buf4=buf4->next;
		delete buf3;
	}
}

void List::output(data *buf)
{
	RussianMessage("\n№ ");
	cout<<buf->nomer;
	RussianMessage(" Фамилия:");
	cout<<buf->name;
	RussianMessage("\n    Возраст:");
	cout<<buf->age;

}
void List::searchONindex()
{
	int index;
	data *buf;

	RussianMessage("\nВведите номер записи:");
	cin >> index;
	buf=head->next;
	while (buf!=NULL)
	{
		if (buf->nomer==index) 
		{
			RussianMessage("Необходимая запись:\n");
			List::output(buf);
			break;
		}
		buf=buf->next;
	}
	if (buf==NULL)
		RussianMessage("Ошибка ... такого индекса не существует...!\n");
}

void List::searchONkey()
{
	int nORa,keyFORage,temp=1,size,i;
	string key;
	data *buf;
	buf=head->next;
	RussianMessage("Задайте поле поиска:");
	RussianMessage(" Фамилия - 1 или возраст - 2\n");
	cin >> nORa;
	RussianMessage("Задайте ключ:\n");
	if (nORa==1)
	cin >> key;
	if (nORa==2)
	cin >> keyFORage;
	switch (nORa)
	{
	case 1:
		RussianMessage("\n\t\tНужная запись:\n");
		while (buf!=NULL)
		{
			size=key.length();
			for (i=0;i<size;i++)
				if (key[i]!=buf->name[i])
				{
					temp=0;
					break;
				}
				else temp=1;
				if (temp!=0)
				{
					temp=1;
					List::output(buf);
				}
				buf=buf->next;
		}
		if (temp==0)
		RussianMessage("Такого ключа нет\n");
		break;
	case 2:
		while (buf!=NULL)
		{
			if (buf->age==keyFORage) 
			{
				RussianMessage("Нужная запись:\n");
				List::output(buf);
				temp=1;
			}
			buf=buf->next;
		}
		if (temp==0)
		RussianMessage("Такого ключа нет\n");
		break;
	default: RussianMessage("Неверно задано поле\n");
			break;
	}
}

void List::add()
{
	string name;
	data *buf,*buf2;
	buf2=new data;
	buf2=NULL;
	int n,age,temp;

	RussianMessage("Введите номер номер списка после которого следует добавить новую запись списка:");
	cin >> n;

	buf=head->next;
	temp=buf->nomer;
	while ((buf->nomer<=n)&&(buf->next!=NULL))
	{
		buf=buf->next;
	}

if (n<=temp)
	{
		temp=buf->nomer;
		//duf2=buf->next;
		buf->next=new data;
		buf=buf->next;

		RussianMessage("\nВведите Фамилию:");
		cin >> name;
		RussianMessage("\nВведите возраст:");
		cin >> age;

		buf->nomer=temp;
		buf->name=name;
		buf->age=age;
		
		buf->next=buf2;	
		
		while (buf!=NULL)
		{
			buf->nomer=++temp;
			buf=buf->next;
		}	
	}
		else RussianMessage("Ошибка...неверно введен номер списка...\n");
}

void List::del()
{
	string name;
	data *buf,*buf2;
	int age,temp;

	buf=head->next;
	temp=buf->nomer;
	buf2=head;

	buf2=buf;
	buf=buf->next;
	temp=buf->nomer;
		 
	buf->next=buf->next	;
	delete buf;
	buf2=buf2->next;
	while (buf2!=NULL)
		{
			buf2->nomer--;
			buf2=buf2->next;
		}	
}

void List::SaveToFile()
{
	string name;
	data *buf;
	buf=head;

	ofstream save("list.txt");
	while (buf->next!=NULL)
	{
		buf=buf->next;
		save<<buf->name;
		save<<"  ";
		save<<buf->age;
	}
}
void List::TakeFromFile()
{
	/*data *buf;
	buf=head;
	string name;*/
	int age;
	FILE* list=fopen("list.txt","r");
	char n[255];
	int a;
	while (!feof(list))
	{
		fscanf(list,"%s",&n);
		fscanf(list,"%i",&a);
		RussianMessage("\nФамилия:\n");
		cout<<n<<"\n";
		RussianMessage("Возраст:\n");
		cout<<a;
	}
	fclose(list);
}
void main()
{
	RussianMessage("\n\t\tЗдравствуйте,введите первую запись:\n");

	List *list;
	list=new List;
	list->create();
	// Основной цикл программы 
	do
	{
		switch(RussianMenu())
		{
			case 1: // Добавление записи
				list->add();
				break;
			case 2: // Показ всех записей
				list->print();
				break;
			case 3:// удалениe всех записей
				list->delList();
				break;
			case 4: // удалениe одного элемента
				list->del();
				break;
			case 5: // поиск по ключу
				list->searchONkey();
				break;
			case 6: // поиск по индексу
				list->searchONindex();
				break;
			case 7:// сохранение в файл
				list->SaveToFile();
				break;
			case 8://считывание из файла
				RussianMessage("\t\tСписок из файла:\n");
				list->TakeFromFile();
				break;
			case 9:    //  Прощание с пользователем
				RussianMessage("До Свидания\n");
				return;
			default: // Неправильный ввод
				RussianMessage("Неверный Ввод\n");
		}

	}while(1);
}


