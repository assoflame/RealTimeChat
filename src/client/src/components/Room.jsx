import React, { useState } from "react";
import RoomUsersList from "./RoomUsersList";
import AdminPanel from "./AdminPanel";

const Room = ({room, messages, sendMessage, roomUsers, adminRights, blockUser}) => {
    const [message, setMessage] = useState('');

    return (
        <div>
            <div>
                {messages.map((message, index) => <div key={index}> {message.createdAt} from {message.senderName} : {message.body}</div>)}
            </div>
            <input placeholder="write message..." value={message} type="text" onChange={e => setMessage(e.target.value)}/>
            <button onClick={e => sendMessage(room, message)}>Send</button>

            <RoomUsersList roomUsers={roomUsers}/>
            { adminRights && <AdminPanel blockUser={blockUser} roomUsers={roomUsers}/> }
        </div>
    );
}

export default Room;