import React, { useState } from "react";

const Room = ({room, messages, sendMessage}) => {
    const [message, setMessage] = useState('');

    return (
        <div>
            <div>
                {messages.map((message, index) => <div key={index}> {message.createdAt} from {message.senderName} : {message.body}</div>)}
            </div>
            <input placeholder="write message..." value={message} type="text" onChange={e => setMessage(e.target.value)}/>
            <button onClick={e => sendMessage(room, message)}>Send</button>
        </div>
    );
}

export default Room;