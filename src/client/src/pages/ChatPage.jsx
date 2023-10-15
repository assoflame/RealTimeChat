import React from "react";
import Chat from "../components/Chat";
import { logout } from "../helpers/Auth";
import { useNavigate } from "react-router-dom";

const ChatPage = () => {
    const navigate = useNavigate();

    return (
        <>
            <header>
                <button className="menuLink" onClick={() => {logout(); navigate('/auth', {replace: true})}}>Logout</button>
            </header>
            <Chat/>
        </>
    )
}

export default ChatPage;