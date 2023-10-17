import React from "react";
import '../styles/Message.css'

const Message = ({message}) => {

    const IsOwnMessage = () => {
        return localStorage.getItem('nickname') === message.senderName;
    }

    return (
            <div className={IsOwnMessage() ? "ownMessage" : "someoneMessage"}>
                <div className={IsOwnMessage() ? "ownMessageBody" : "someoneMessageBody"}>{message.body}</div>
                <div className={IsOwnMessage() ? "ownMessageSender" : "someoneMessageSender"}>{message.senderName}</div>
            </div>
    )
}

export default Message;