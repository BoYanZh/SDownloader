# -*- coding: utf-8 -*-
from bs4 import BeautifulSoup
import urllib
import sys
import hashlib
import os

DOMAIN = "http://www.ppp179.com/"

def getHtml(url, timeout = 10):
    global DOMAIN
    try:
        if("http" not in url):
            url = DOMAIN + url
        user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'
        headers = { 'User-Agent' : user_agent }
        req = urllib.request.Request(url, headers = headers)
        response = urllib.request.urlopen(req)
        #page = urllib.request.urlopen(url, timeout = timeout)
        html = response.read().decode('utf-8')
        return html
    except:
        print("Unexpected error:", sys.exc_info()[0])
        raise
        return ""
    
def getIndexPageList(html):
    global DOMAIN
    KEY_WORD = "欧美"
    pageList = []
    soup = BeautifulSoup(html, "lxml")
    for link in soup.find_all('a'):
        if(link.string != None and KEY_WORD in link.string):
            url = link.get('href').replace(DOMAIN, "")
            if(url not in pageList):
                pageList.append(url)
    return pageList

def getContentPageList(url):
    global DOMAIN
    KEY_URL = "p03/41"
    pageList = []
    html = getHtml(url)
    soup = BeautifulSoup(html, "lxml")
    for link in soup.find_all('a'):
        if(KEY_URL in link.get('href')):
            url = link.get('href').replace(DOMAIN, "")
            if(url not in pageList):
                pageList.append(link.get('href'))
    return pageList

def getImgUrlList(url):
    global DOMAIN
    imgList = []
    html = getHtml(url)
    soup = BeautifulSoup(html, "lxml")
    for link in soup.find_all('img'):
        url = link.get('src')
        if(url not in imgList):
            imgList.append(link.get('src'))
    return imgList
    
def downloadImg(url):
    global DOMAIN
    if("http" not in url):
        url = DOMAIN + url
    filePath = DOMAIN.split(".")[1] +  "\\" + hashlib.md5(url.split("/")[-2].encode("utf-8")).hexdigest()
    fileName = url.split("/")[-1]
    fileFullPath = filePath + "\\" + fileName
    response = urllib.request.urlopen(url)
    urlImg = response.read()
    if(len(urlImg) > 10000):
        if not os.path.exists(filePath):
            os.makedirs(filePath) 
        with open(fileFullPath,'wb') as f:
            f.write(urlImg)
    return fileFullPath

html = getHtml(DOMAIN)
indexPageList = getIndexPageList(html)
contentPageList = getContentPageList(indexPageList[0])
for contentPage in contentPageList:
    imgUrlList = getImgUrlList(contentPage)
    for imgUrl in imgUrlList:
        print(downloadImg(imgUrl))