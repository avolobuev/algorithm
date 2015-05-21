#include <stdio.h>
int my_atoi(char *s)
{
	int rt=0;
	bool neg = false;
	if(*s=='-')
	{
		neg = true;
		*s++;
	}
	while(*s)
	{
		if(*s<'0' || *s>'9')
		{
			printf("error! unexpected char: %c\n", *s);
			break;
		}
		rt=rt*10 + (*s-'0');
		s++;
	}
	if(neg)
		return -rt;
	return rt;
}
int main()
{
	printf("%d\n",my_atoi("1234"));
	return 0;
}