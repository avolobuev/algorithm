#include <iostream>
#include <string>
#include <vector>
#include <map>
#include <sstream>

const int nB = 25; 

int _countRatesForItem(int nItem, std::map<int, std::map<int, int> > vUMR)
{
int nResult = 0; 

for(std::map<int, std::map<int, int> >::iterator itU = vUMR.begin(); itU != vUMR.end(); ++itU)
{
if(vUMR[itU->first][nItem] != NULL)
{
nResult++;
  }
}

return nResult;
}

float _mean(std::map<int, std::map<int, int> > vUMR)
{

float nResult = 0.0f; 

int nCount = 0; 

std::map<int, std::map<int, int> >::iterator itU; 

std::map<int, int>::iterator itM; 

for(itU = vUMR.begin(); itU != vUMR.end(); ++itU)
{
for(itM = vUMR[itU->first].begin(); itM != vUMR[itU->first].end(); ++itM)
{
nResult += vUMR[itU->first][itM->first];

nCount++;
  }
}

if(nCount != 0) nResult /= nCount;

return nResult;
}

float bU(int nUser, std::map<int, std::map<int, int> > vUMR, float fMean)
{
float fResult = 0.0f;

int nIu = vUMR[nUser].size();

for(std::map<int, int>::iterator itM = vUMR[nUser].begin(); itM != vUMR[nUser].end(); ++itM)
{
fResult += vUMR[nUser][itM->first] - fMean;
}

fResult /= (nIu + nB); 

return fResult;
}

float bM(int nItem, std::map<int, std::map<int, int> > vUMR, float fMean)
{

float fResult = 0.0f;

int nIu = _countRatesForItem(nItem, vUMR);

for(std::map<int, std::map<int, int> >::iterator itU = vUMR.begin(); itU != vUMR.end(); ++itU)
{
if(vUMR[itU->first][nItem] != NULL)
{
fResult += vUMR[itU->first][nItem] - bU(itU->first, vUMR, fMean) - fMean;  
  }
}

fResult /= (nIu + nB);

return fResult;
}

std::vector<int> _explode(std::string sIn)
{
int nCurrent = 0;

std::istringstream iss(sIn);

std::vector<int> vResult;

do
{
iss >> nCurrent;

vResult.push_back(nCurrent);
} while (iss);

return vResult;
}

int main(int argc, char** argv) 
{
std::string sMainParams = "";

std::getline(std::cin, sMainParams);

if(sMainParams != "")
{
int nI = 0, nJ = 0;

std::vector<int> vMainParams = _explode(sMainParams);

int nK = vMainParams[0];

int nU = vMainParams[1];

int nM = vMainParams[2];

int nD = vMainParams[3];

int nT = vMainParams[4];

vMainParams.clear();

std::string sTemp = "";

std::vector<std::string> sDSet;

std::vector<std::string> sTSet;

std::map<int, std::map<int, int> > vUMR;

for(nI = 0; nI < nD; ++nI)
{
std::getline(std::cin, sTemp);

sDSet.push_back(sTemp);
  }
for(nI = 0; nI < nT; ++nI)
{

std::getline(std::cin, sTemp);

sTSet.push_back(sTemp);  
}  

std::map<int, int> vMR;

std::vector<int> vTemp;

for(nI = 0; nI < nU; ++nI)
{

for(nJ = 0; nJ < nD; ++nJ)
{
vTemp = _explode(sDSet[nJ]);

if(nI == vTemp[0])
{
vMR[vTemp[1]] = vTemp[2];
  }
}

vUMR[nI] = vMR;

vMR.clear();
}

sDSet.clear();

vTemp.clear();

int nTSet = sTSet.size();

float fResult = 0.0f;

float fMean = _mean(vUMR);

for(nI = 0; nI < nTSet; ++nI)
{

fResult = fMean + bU(_explode(sTSet[nI])[0], vUMR, fMean) + bM(_explode(sTSet[nI])[1], vUMR, fMean);

std::cout << fResult << std::endl;
  }

sTSet.clear();
}

return 0;
}
