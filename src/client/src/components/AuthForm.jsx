import React, { useState } from "react";
import '../styles/AuthForm.css'
import { signIn, signUp } from "../helpers/Auth";
import { useNavigate } from "react-router-dom";


const AuthForm = () => {
    const [nickname, setNickname] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();
    const chatPage = '/chat'

    return (
        <form className="authForm" onSubmit={e => e.preventDefault()}>
                <div className="authInputs">
                    <input className="authInput" placeholder='nickname' onChange={e => setNickname(e.target.value)}/>
                    <input className="authInput" placeholder='password' type="password" onChange={e => setPassword(e.target.value)}/>
                </div>
            <button className="authButton" type="submit"
                onClick={async () => {
                        await signIn(nickname, password);
                        navigate(chatPage, {replace: true});
                        }}>
                            Sign In
            </button>
            <button className="authButton" type="submit"
                onClick={async () => {
                        await signUp(nickname, password);
                        navigate(chatPage, {replace: true});
                        }}>
                            Sign Up
            </button>
        </form>
    )
}

export default AuthForm;