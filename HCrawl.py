# -*- coding: utf-8 -*-
from bs4 import BeautifulSoup
import urllib
import sys
import os

DOMAIN = "http://www.ppp179.com/"
KEY_WORD = ""
UNALLOWED_DICT = {}

def getHtml(url, timeout = 10):
    global DOMAIN
    try:
        if("http" not in url):
            url = DOMAIN + url
        user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
        headers = { 'User-Agent' : user_agent }
        req = urllib.request.Request(url, headers = headers)
        response = urllib.request.urlopen(req, timeout = timeout)
        html = response.read().decode('utf-8')
        return html
    except:
        print("Unexpected error:", sys.exc_info()[0], "Url", url)
        return ""

#返回含关键词的的索引页dict
def getIndexPageDict(html):
    global DOMAIN
    PAGE_KEY_WORD = "欧美"
    #pageDict = []
    soup = BeautifulSoup(html, "lxml")
    for link in soup.find_all('a'):
        if(link.string != None and PAGE_KEY_WORD in link.string):
            tmpDict = {"keyWord": link.string, \
                       "url": link.get('href').replace(DOMAIN, "")}
            return tmpDict
    return {}
            #if(tmpDict not in pageDict):
            #    pageDict.append(tmpDict)
    #return pageDict

#返回内容页名称+链接dict
def getContentPageDict(indexPageDict):
    global DOMAIN, UNALLOWED_DICT
    if(UNALLOWED_DICT.get(indexPageDict['keyWord']) == None):
        UNALLOWED_DICT[indexPageDict['keyWord']] = []
    if(indexPageDict['url'] in UNALLOWED_DICT[indexPageDict['keyWord']]):
        return {}
    else:
        pageDict = []
        html = getHtml(indexPageDict["url"])
        soup = BeautifulSoup(html, "lxml")
        for link in soup.find_all('a'):
            tmpDict = {"keyWord": indexPageDict["keyWord"], \
                       "name": link.string, \
                       "url": link.get('href').replace(DOMAIN, "")}
            if(tmpDict["name"] != None and len(tmpDict["name"]) > 6 and tmpDict not in pageDict):
                pageDict.append(tmpDict)
        return pageDict

#返回内容页中图片链接dict
def getImgUrlDict(contentPageDict):
    global DOMAIN, UNALLOWED_DICT
    imgDict = []
    html = getHtml(contentPageDict["url"])
    soup = BeautifulSoup(html, "lxml")
    for link in soup.find_all('img'):
        tmpDict = {"keyWord": contentPageDict["keyWord"], \
                   "name": contentPageDict["name"], \
                   "url": link.get('src')}
        if(tmpDict not in imgDict):
            imgDict.append(tmpDict)
    if(len(imgDict) > 10):
        return imgDict
    else:
        UNALLOWED_DICT[contentPageDict['keyWord']].append(contentPageDict["url"])
        return []

#下载图片
def downloadImg(imgUrlDict):
    global DOMAIN
    if("http" not in imgUrlDict["url"]):
        imgUrlDict["url"] = DOMAIN + imgUrlDict["url"]
    filePath = DOMAIN.split(".")[1] +  "\\" + imgUrlDict["keyWord"] +  "\\" + imgUrlDict["name"]
    fileName = imgUrlDict["url"].split("/")[-1]
    fileFullPath = filePath + "\\" + fileName
    response = urllib.request.urlopen(imgUrlDict["url"], timeout = 10)
    urlImg = response.read()
    if(len(urlImg) > 10000):
        try:
            if not os.path.exists(filePath):
                os.makedirs(filePath) 
            with open(fileFullPath,'wb') as f:
                f.write(urlImg)
            return fileFullPath
        except:
            return "Unexpected error:", sys.exc_info()[0]
    else:
        return ""

def getNextIndexPageDict(indexPageDict):
    global DOMAIN
    html = getHtml(indexPageDict["url"])
    soup = BeautifulSoup(html, "lxml")
    for link in soup.find_all('a'):
        if(link.string != None and "下一页" in link.string):
            tmpDict = {"keyWord": indexPageDict["keyWord"], \
                       "url": link.get('href').replace(DOMAIN, "")}
            return tmpDict
    return {}

html = getHtml(DOMAIN)
indexPageDict = getIndexPageDict(html)
while(indexPageDict != {}):
    contentPageDict = getContentPageDict(indexPageDict)
    print(contentPageDict)
    for contentPage in contentPageDict:
        imgUrlDict = getImgUrlDict(contentPage)
        for imgUrl in imgUrlDict:
            print(downloadImg(imgUrl))
    indexPageDict = getNextIndexPageDict(indexPageDict)
    print("\nnext page\n")