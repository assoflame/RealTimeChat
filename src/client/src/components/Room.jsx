import React, { useState } from "react";
import RoomUsersList from "./RoomUsersList";
import AdminPanel from "./AdminPanel";
import '../styles/App.css'
import Message from "./Message";

const Room = ({room, messages, sendMessage, roomUsers, adminRights, blockUser}) => {
    const [message, setMessage] = useState('');

    return (
        <div id="room">
            <div id="roomMain">
                <div id="currentRoomName">{room}</div>

                <div id="messages">
                    {messages.map((message, index) => <Message key={index} message={message}/>)}
                </div>

                <div id="writeMessagePanel">
                    <textarea id="messageInput" placeholder="Write message..." value={message} type="text" onChange={e => setMessage(e.target.value)}/>
                    <button id="sendMessageButton" onClick={e => sendMessage(room, message)}>Send</button>
                </div>
            </div>

            {/* <RoomUsersList roomUsers={roomUsers}/>
            { adminRights && <AdminPanel blockUser={blockUser} roomUsers={roomUsers}/> } */}
        </div>
    );
}

export default Room;