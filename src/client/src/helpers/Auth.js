
export const signIn = async (nickname, password) => {
    let response = await fetch("http://localhost:5285/api/auth/signin", {
      method: "POST",
      headers: {
        "Content-Type" : "application/json"
      },
      body: JSON.stringify({nickname, password})
    });

    if(response.ok){
      let result = await response.json();
      document.cookie = `accessToken=${result.accessToken}; expires=${new Date(Date.now() + 86400 * 1000).toUTCString()}`;
      localStorage.setItem('nickname', nickname);
    } else {
      console.log("error");
    }
  }

  export const signUp = async (nickname, password) => {
    let response = await fetch("http://localhost:5285/api/auth/signup", {
      method: "POST",
      headers: {
        "Content-Type" : "application/json"
      },
      body: JSON.stringify({nickname, password})
    });

    if(response.ok){
      await signIn(nickname, password);
    } else {
      console.log("error");
    }
  }

  export const getAccesToken = () => {
    let cookies = document.cookie.split('; ');
    for (let i = 0; i < cookies.length; ++i)
    {
      if(cookies[i].startsWith('accessToken')) {
        console.log(cookies[i].split('=')[1]);
        return cookies[i].split('=')[1];
      }
    }
    return '';
  }

  export const logout = () => {
    localStorage.removeItem('nickname');
    document.cookie = `accessToken= ; expires=${new Date(Date.now()).toUTCString()}`;
    console.log(document.cookie);
  }

  export const loggedIn = () => {
    // let nickname = localStorage.getItem('nickname');
    return localStorage.getItem('nickname') !== null;
  }
