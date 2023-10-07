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
      // document.cookie = `accessToken=${result.accessToken}; expires=${result.expiresIn}`;
      document.cookie = `accessToken=${result.accessToken}`;
      console.log(document.cookie);
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
      let result = await response.json();
      console.log(JSON.stringify(result));
    } else {
      console.log("error");
    }
  }