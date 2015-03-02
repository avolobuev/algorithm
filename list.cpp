// ��������3.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"


void RussianMessage(char *message)//������� �����������
{
	char rmessage[256];
	CharToOem(message,rmessage);
	cout<<rmessage;
}

int RussianMenu()//������� ������ ������
{
	RussianMessage("\n������� 1 ��� ���������� ����� ������ � ����\n");
	RussianMessage("������� 2 ��� ������ ���� �������\n");
	RussianMessage("������� 3 ��� �������� ���� �������\n");
	RussianMessage("������� 4 ��� �������� ������ ��������\n");
	RussianMessage("������� 5 ��� ������ �� �����\n");
	RussianMessage("������� 6 ��� ������ �� �������\n");
	RussianMessage("������� 7 ��� ������ ������ � ����\n");
	RussianMessage("������� 8 ��� ���������� �� �����\n");
	RussianMessage("������� 9 ��� ������\n");
	int choice;
	cin>>choice;
	return choice;
}
class List // ����� ����� ������������ ������
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
		List()// �����������
		{
			head=new data;
			head->next=NULL; 
		}                     
		~List()// ����������
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
		void SaveToFile();// ������� ���������� � ���� �����
		void TakeFromFile();
};

void List::create()
{
	data *buf;
	string name;
	buf=head;
	int kp=0,age;
		
	kp++;
	RussianMessage("������� �������:");
	cin >> name;
	RussianMessage("\n������� �������:");
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
	RussianMessage("\n\t\t������:\n");
	while (buf!=NULL)
	{
		List::output(buf);
		buf=buf->next;// ��������� �� ��������� �������
	}
	cout<<endl;
}

void List::delList()
{
	data *buf3,*buf4;
	buf3=head;
	buf4=buf3->next;
	while(buf4!=NULL)// ���� ���� �������� ������� �� �� ������
	{	
		buf3=buf4;
		buf4=buf4->next;
		delete buf3;
	}
}

void List::output(data *buf)
{
	RussianMessage("\n� ");
	cout<<buf->nomer;
	RussianMessage(" �������:");
	cout<<buf->name;
	RussianMessage("\n    �������:");
	cout<<buf->age;

}
void List::searchONindex()
{
	int index;
	data *buf;

	RussianMessage("\n������� ����� ������:");
	cin >> index;
	buf=head->next;
	while (buf!=NULL)
	{
		if (buf->nomer==index) 
		{
			RussianMessage("����������� ������:\n");
			List::output(buf);
			break;
		}
		buf=buf->next;
	}
	if (buf==NULL)
		RussianMessage("������ ... ������ ������� �� ����������...!\n");
}

void List::searchONkey()
{
	int nORa,keyFORage,temp=1,size,i;
	string key;
	data *buf;
	buf=head->next;
	RussianMessage("������� ���� ������:");
	RussianMessage(" ������� - 1 ��� ������� - 2\n");
	cin >> nORa;
	RussianMessage("������� ����:\n");
	if (nORa==1)
	cin >> key;
	if (nORa==2)
	cin >> keyFORage;
	switch (nORa)
	{
	case 1:
		RussianMessage("\n\t\t������ ������:\n");
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
		RussianMessage("������ ����� ���\n");
		break;
	case 2:
		while (buf!=NULL)
		{
			if (buf->age==keyFORage) 
			{
				RussianMessage("������ ������:\n");
				List::output(buf);
				temp=1;
			}
			buf=buf->next;
		}
		if (temp==0)
		RussianMessage("������ ����� ���\n");
		break;
	default: RussianMessage("������� ������ ����\n");
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

	RussianMessage("������� ����� ����� ������ ����� �������� ������� �������� ����� ������ ������:");
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

		RussianMessage("\n������� �������:");
		cin >> name;
		RussianMessage("\n������� �������:");
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
		else RussianMessage("������...������� ������ ����� ������...\n");
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
		RussianMessage("\n�������:\n");
		cout<<n<<"\n";
		RussianMessage("�������:\n");
		cout<<a;
	}
	fclose(list);
}
void main()
{
	RussianMessage("\n\t\t������������,������� ������ ������:\n");

	List *list;
	list=new List;
	list->create();
	// �������� ���� ��������� 
	do
	{
		switch(RussianMenu())
		{
			case 1: // ���������� ������
				list->add();
				break;
			case 2: // ����� ���� �������
				list->print();
				break;
			case 3:// �������e ���� �������
				list->delList();
				break;
			case 4: // �������e ������ ��������
				list->del();
				break;
			case 5: // ����� �� �����
				list->searchONkey();
				break;
			case 6: // ����� �� �������
				list->searchONindex();
				break;
			case 7:// ���������� � ����
				list->SaveToFile();
				break;
			case 8://���������� �� �����
				RussianMessage("\t\t������ �� �����:\n");
				list->TakeFromFile();
				break;
			case 9:    //  �������� � �������������
				RussianMessage("�� ��������\n");
				return;
			default: // ������������ ����
				RussianMessage("�������� ����\n");
		}

	}while(1);
}


