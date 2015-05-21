#include <stdio.h>
#include <stdint.h>

void print_as_IP(uint32_t address)
{
	printf("%d.%d.%d.%d\n", (address>>24)&0xFF,(address>>16)&0xFF,(address>>8)&0xFF,(address)&0xFF);
}

uint32_t form_IP(uint8_t ip1, uint8_t ip2, uint8_t ip3,uint8_t ip4)
{
	return (ip1<<24) | (ip2<<16) | (ip3<<8) | ip4;
}

uint32_t set_bit(uint32_t input, int bit)
{
	return input|=(1<<bit);
}

uint32_t form_netmask(uint8_t netmask_bits)
{
	uint32_t netmask=0;
	for(uint8_t i = 0; i < netmask_bits; ++i)
	{
		netmask = set_bit(netmask, 31-i);
	}
	return netmask;
}

void calc_network_address(uint8_t ip1, uint8_t ip2, uint8_t ip3,uint8_t ip4, uint8_t netmask_bits)
{
	uint32_t netmask = form_netmask(netmask_bits);
	uint32_t ip = form_IP(ip1,ip2,ip3,ip4);
	uint32_t netw_adr = ip & netmask;
	printf("\nnetmask=");
	print_as_IP(netmask);
	printf("\nnetwork address=");
	print_as_IP(netw_adr);
}
int main()
{
calc_network_address(10,1,2,4,24);//24
calc_network_address(10,1,2,4,8);//24
calc_network_address(10,1,2,4,25);//24
calc_network_address(10,1,2,64,26);//24
return 0;
}