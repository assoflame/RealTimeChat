import React from "react";

const AdminPanel = ({roomUsers, banUser}) => {
    return (
        <div>
            {roomUsers.filter((username) => username !== localStorage.getItem('nickname'))
                .map((user, index) => {
                    return <div key={index}>
                        <div>{user}</div>
                        <button onClick={e => banUser(user)}>Block</button>
                    </div>;})
            }
        </div>
    )
}

export default AdminPanel;
