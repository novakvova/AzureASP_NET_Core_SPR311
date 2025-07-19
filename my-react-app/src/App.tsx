import './App.css'
import {useGoogleLogin} from "@react-oauth/google";

function App() {

    const loginByGoogle = useGoogleLogin({
        onSuccess: tokenResponse => {
            console.log("Get Google token",tokenResponse)
        },
    });

  return (
    <>
      <h1 style={{textAlign: "center"}}>Насолоджуємося кодом</h1>
        <button onClick={() => loginByGoogle()}>Sign in with Google 🚀</button>
    </>
  )
}

export default App
