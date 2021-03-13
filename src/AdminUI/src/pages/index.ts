import Homepage from './homepage/Homepage';
import Login from './login/Login';
import ForgetPassword from './login/forgetPassword';
import ErrorPages from './Error';
import UserPages from './user/index';
import ProductPages from './product/index';
import RackPages from './rack/index';
import TrailerPages from './trailer/index';
import TruckPages from './truck/index';
import VendorPages from './vendor/index';
import DriverScheduleList from './driverSchedule/index';

const pages = { 
    Homepage, 
    ErrorPages, 
    Login, 
    ForgetPassword,
    UserPages,
    ProductPages,
    RackPages,
    TrailerPages,
    TruckPages,
    VendorPages,
    DriverScheduleList
};

export default pages;
