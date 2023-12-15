import { useState,useEffect} from "react";

function useResize(){
    const [mobileWidth,setMoblieWidth] = useState(false);
    const onResize=()=>{
        if(window.innerWidth<992)
        {setMoblieWidth(true)}
        else
        {setMoblieWidth(false)}
    }

    useEffect(()=>{
    onResize()
    window.addEventListener("resize",onResize)
    return () =>window.removeEventListener("resize",onResize)
    
    },[])

    return mobileWidth;
}

export default useResize;