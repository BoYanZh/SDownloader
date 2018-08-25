from urllib.request import Request, urlopen
from urllib.error import URLError
import threading as td
import random
import re
import time
import queue

PATTERN = re.compile(r'成人|色|视频|电影|照片')

def validate(url, q):
    """
    Judge whether url is the website we need.
    """
    headers = {'User-Agent':'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'}
    try:
        request = Request(url, headers=headers)
        html = urlopen(request, timeout=4).read().decode('utf-8')
        if re.search(PATTERN, html):
            q.put((url, html))
        else:
            return
    except UnicodeDecodeError:
        try:
            request = Request(url, headers=headers)
            html = urlopen(request, timeout=4).read().decode('gbk', 'replace')
            if re.search(PATTERN, html):
                q.put((url, html))
            else:
                return
        except:
            return
    except:
        return



def gen():
    """
    Generate candidates
    """
    case = random.choice([0,1,2])
    if case == 0:
        l=random.randint(0,9)
        kl=random.randint(97,122)
        ll=random.randint(97,122)
        return 'http://www.'+str(l)+str(l)+chr(kl)+chr(ll)+chr(kl)+chr(ll)+'.com'
    elif case == 1:
        l=random.randint(97,122)
        jl=random.randint(0,9)
        kl=random.randint(0,9)
        ll=random.randint(0,9)
        return 'http://www.'+chr(l)+chr(l)+chr(l)+str(jl)+str(kl)+str(ll)+'.com'
    elif case == 2:
        l=random.randint(97,122)
        kl=random.randint(0,9)
        jl=random.randint(0,9)
        return 'http://www.'+chr(l)+chr(l)+str(jl)+str(kl)+str(jl)+str(kl)+'.com'


PROCESSES = 64

if __name__ == "__main__":
    base_active_count = td.active_count()
    url_set = set()
    html_set = set()
    q = queue.Queue()
    while True:
        try:
            while td.active_count() - base_active_count < PROCESSES:
                url = gen()
                if url not in url_set:
                    p = td.Thread(target=validate, args=(url, q)).start()
                    url_set.update(url)
                while not q.empty():
                    url, html = q.get()
                    if html not in html_set:
                        print(url)
                        html_set.update(html)
            time.sleep(0.01)

        except KeyboardInterrupt:
            break
