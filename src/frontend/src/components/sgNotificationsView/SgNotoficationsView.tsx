import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useSnackbar } from 'notistack';
import {hideToast, selectToasts} from "../../redux/slices/toasts/toastsSlice";

let displayingNotificationsIds: string[] = [];

const SgNotificationsView = () => {
    const dispatch = useDispatch();
    const toasts = useSelector(selectToasts);
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    const storeDisplaying = (id: string) => {
        displayingNotificationsIds = [...displayingNotificationsIds, id];
    };

    const removeDisplaying = (id: string) => {
        displayingNotificationsIds = [...displayingNotificationsIds.filter(key => id !== key)];
    };

    React.useEffect(() => {
        toasts.forEach(toast => {
            if (toast.isShown) {
                // dismiss snackbar using notistack
                if (displayingNotificationsIds.includes(toast.id))
                {
                    closeSnackbar(toast.id);
                    removeDisplaying(toast.id);
                }
                   
                return;
            }

            // do nothing if snackbar is already displayed
            if (displayingNotificationsIds.includes(toast.id)) return;

            // display snackbar using notistack
            enqueueSnackbar(toast.message, {
                key: toast.id,
                variant: toast.messageType,
                //...options,
                onClose: (event, reason, myKey) => {
                    // if (options.onClose) {
                    //     options.onClose(event, reason, myKey);
                    // }
                    dispatch(hideToast(toast.id));
                    removeDisplaying(toast.id);
                },
                onExited: (event, myKey) => {
                    // remove this snackbar from redux store
                    dispatch(hideToast(toast.id));
                    removeDisplaying(toast.id);
                },
            });

            // keep track of snackbars that we've displayed
            storeDisplaying(toast.id);
        });
    }, [toasts, closeSnackbar, enqueueSnackbar, dispatch]);
};

export default SgNotificationsView;
