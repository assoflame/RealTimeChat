import 'bootstrap/dist/css/bootstrap.min.css'
import './styles/App.css'
import AuthForm from './components/AuthForm';
import { Navigate, Route, Routes } from 'react-router-dom';
import RequireAuth from './helpers/RequireAuth';
import ChatPage from './pages/ChatPage';

const App = () => {
  return (
    <div>
        <Routes>
          <Route path='/auth' element={<AuthForm/>}/>
          <Route path='/chat' element={
              <RequireAuth>
                <ChatPage/>
              </RequireAuth>}/>
          <Route path='*' element={<Navigate to={'/chat'}/>}/>
        </Routes>
    </div>
  );
}

export default App;
