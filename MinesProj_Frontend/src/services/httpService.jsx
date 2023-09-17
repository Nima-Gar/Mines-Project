import axios from "axios";
import { toast } from "react-toastify";

axios.defaults.headers.post["Content-Type"] = "application/json";

axios.interceptors.response.use(null, error => {
    const expectedError = error.response &&  error.response.status >= 400 &&  error.response.status < 500
    if (! expectedError) {
        console.log(error);
        toast.error("مشکلی از سمت سرور رخ داده است.")
    }

    return Promise.reject(error)
})

export default {
    get: axios.get,
    post: axios.post,
    put: axios.put,
    delete: axios.delete
}