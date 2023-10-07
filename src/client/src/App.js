import 'bootstrap/dist/css/bootstrap.min.css'
import './styles/App.css'
import AuthForm from './components/AuthForm';
import { Link, Route, Routes } from 'react-router-dom';
import { signIn, signUp } from './helpers/Auth';
import Chat from './components/Chat';

const App = () => {
  return (
    <div>
      <Link to={'auth/signin'}>Sign In</Link>
      <Link to={'auth/signup'}>Sign Up</Link>
      <Link to={'chat'}>Chat</Link>
        <Routes>
          <Route path='/auth/signin' element={<AuthForm actionName={'Sign in'} action={signIn}/>}/>
          <Route path='/auth/signup' element={<AuthForm actionName={'Sign up'} action={signUp}/>}/>
          <Route path='/chat' element={<Chat/>}/>
        </Routes>
    </div>
    
  );
}

export default App;
