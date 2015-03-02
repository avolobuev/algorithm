#include "stdafx.h"
#include "VolobuevL1.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CWinApp theApp;

using namespace std;

HANDLE hFileMap;
HANDLE hFile;

int menu();
void xor(BYTE* buf, char* password,unsigned long size);

int _tmain(int argc, TCHAR* argv[], TCHAR* envp[])
{
	int nRetCode = 0;

	HMODULE hModule = ::GetModuleHandle(NULL);

	if (hModule!=NULL)
	{
		// инициализировать MFC, а также печать и сообщения об ошибках про сбое
		if (!AfxWinInit(hModule, NULL, ::GetCommandLine(), 0))
		{
			// TODO: измените код ошибки соответственно своим потребностям
			_tprintf(_T("Критическая ошибка: сбой при инициализации MFC\n"));
			nRetCode = 1;
		}
		else
		{
			int key;				
			char* BeginF;
			BeginF=new char[];
			char* EndF;
			EndF=new char[];
			do
		    {
				key=menu();
				switch(key)
				{
				case 1:
					{
						cout<<"Enter the name of source file: ";
						cin>>BeginF;
						cout<<"Enter the name of resulted file: ";
						cin>>EndF;
						if (!CopyFile(BeginF,EndF,FALSE))
						{
							cout<<"File was not found!"<<endl;
						}
						else 
						{
							hFile=CreateFile(EndF,GENERIC_READ|GENERIC_WRITE,0,NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);
							DWORD SizeF=GetFileSize(hFile,NULL);
							hFileMap=CreateFileMapping(hFile,NULL,PAGE_READWRITE,0,NULL,NULL);
							if (!hFileMap)
							{
							   cout<<"Error of creation file mapping"<<endl; 
							}
							BYTE *buf;
							buf=new BYTE;
							buf=(BYTE*)MapViewOfFile(hFileMap,FILE_MAP_WRITE,0,0,0);

							char *password;
							password=new char;
							*password=NULL;
							cout<<"Enter the password: ";
							cin>>password;
							xor(buf,password,unsigned long(SizeF));
							cout<<"File is changed!"<<endl;

							UnmapViewOfFile(hFileMap);
							CloseHandle(hFileMap);
							CloseHandle(hFile);
						}
						break;
					}
				case 2:
					{
						setlocale(LC_ALL,"rus");
						char* name=new char[];
						cout<<"Input name of file:";
						cin>>name;
						ifstream infile(name);
						cout<<infile.rdbuf();
						cout<<endl;
						break;
					}
				case 3:
					{
						return 0;
					}
				default:
					{
						cout<<"Error!"<<endl;
						return 0;
					}
				}
			}while(key!=3);
		}
	}
	else
	{
		// TODO: Измените код ошибки соответственно своим потребностям
		_tprintf(_T("Критическая ошибка: неудачное завершение GetModuleHandle\n"));
		nRetCode = 1;
	}	
	return nRetCode;
}
int menu()
{
	int choice;
	cout<<"1. To cipher or decipher."<<endl;
	cout<<"2. Open file."<<endl;
	cout<<"3. Close."<<endl;
	cout<<"Choose:";
	cin>>choice;
	return choice;
}
void xor(BYTE* buf,char* password,unsigned long size)
{
	unsigned int j;
	for(unsigned int i=0;i<size;i++)
	{
		j=0;
		while(password[j])
		{
			buf[i]^=(password[j]+(i*j));
			j++;
		}
	}
}