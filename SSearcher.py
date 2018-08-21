from urllib.request import Request, urlopen
import multiprocessing as mp
import random
import re

PATTERN = re.compile(r'成人|色|视频|电影|照片')

def validate(url):
    """
    Judge whether url is the website we need.
    """
    headers = {'User-Agent':'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'}
    try:
        request = Request(url, headers=headers)
        html = urlopen(request, timeout=4).read().decode('utf-8')
        if re.search(PATTERN, html):
            print(url)
        else:
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
        jl=random.randint(97,122)
        kl=random.randint(97,122)
        ll=random.randint(97,122)
        return 'http://www.'+str(l)+str(l)+str(l)+chr(jl)+chr(kl)+chr(ll)+'.com'
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

if __name__ == "__main__":
    pool = mp.Pool(processes=64)
    try:
        while True:
            page_urls = [gen() for _ in range(1000)]
            results = pool.map(validate, page_urls)
    except KeyboardInterrupt:
        exit()
