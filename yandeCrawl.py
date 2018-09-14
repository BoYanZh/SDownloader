# -*- coding: utf-8 -*-
import re
import urllib
import threading as td
import sys
import os

IMG_ID = 0
THREAD_COUNT = 10
START_ID = 0
END_ID = 0
INDEX_ID = 0
STOP_FLAG = False

def threadTask(threadId):
    global STOP_FLAG, START_ID, END_ID, THREAD_COUNT, IMG_ID
    print("thread " + str(threadId) + " start")
    while IMG_ID <= END_ID:
        lock.acquire()
        IMG_ID += 1
        lock.release()
        getImg(IMG_ID)
        if STOP_FLAG:
            break
    print("thread " + str(threadId) + " end")

def getImg(imgId):
    #imgId = random.randint(400000, 476581)
    if(os.path.exists("output\\" + str(imgId) + ".jpg")):
        return ""
    html = getHtml("http://yande.re/post/show/" + str(imgId))
    imgUrl = getUrl(html)
    res = downloadImg(imgUrl, imgId)
    return res
        
def getHtml(url, timeout = 10):
    headers = { 'User-Agent' : 'Mozilla/5.0 (Windows NT 10.0;) Gecko/20100101 Firefox/61.0' }
    req = urllib.request.Request(url, headers = headers)
    try:
        response = urllib.request.urlopen(req, timeout = timeout)
        try:
            html = response.read().decode('utf-8')
            return html
        except UnicodeDecodeError:
            html = response.read().decode("gbk")
            return html
        except:
            print("Unexpected error:" + str(sys.exc_info()) + "Url:" + url)
            return ""
    except:
        print("Unexpected error:" + str(sys.exc_info()) + "Url:" + url)
        return ""

def getUrl(html):
    PATTERN = 'large_height="\d+" src="(.+?.jpg)"'
    reRes = re.search(PATTERN, html)
    if(reRes):
        imgUrl = reRes.group(1)
        return imgUrl
    else:
        return ""
        
def downloadImg(imgUrl, imgId):
    if(len(imgUrl) == 0):
        return ""
    filePath = "output"
    fileName = str(imgId) + ".jpg"
    fileFullPath = filePath + "\\" + fileName
    try:
        if(not os.path.exists(filePath)):
            os.makedirs(filePath)
        try:
            headers = { 'User-Agent' : 'Mozilla/5.0 (Windows NT 10.0;) Gecko/20100101 Firefox/61.0' }
            req = urllib.request.Request(imgUrl, headers = headers)
            response = urllib.request.urlopen(req, timeout = 10)
            urlImg = response.read()
            with open(fileFullPath,'wb') as f:
                f.write(urlImg)
            print("Download Success:" + fileFullPath)
            return fileFullPath
        except:
            print("Unexpected error:" + str(sys.exc_info()[1]) + "imgUrl:" + imgUrl)
            return ""
    except:
        print("Unexpected error:" + str(sys.exc_info()[1]) + "imgUrl:" + imgUrl)
        return ""

lock=td.Lock()
IMG_ID = int(input("Input start ID:"))
END_ID = int(input("Input end ID:"))
for i in range(THREAD_COUNT):
    t = td.Thread(target=threadTask, args=(i,), daemon=True)
    t.start()
s = ""
while(s != "stop" and not STOP_FLAG):
    s = input("")
STOP_FLAG = True
print("main thread end")