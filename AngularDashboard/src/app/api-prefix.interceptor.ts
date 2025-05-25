import { HttpInterceptorFn } from '@angular/common/http';

const baseUrl = 'https://localhost:7156/api/';

export const apiPrefixInterceptor: HttpInterceptorFn = (req, next) => {
  // Only prepend if the URL is relative (does not start with http or https)
  if (!/^https?:\/\//i.test(req.url)) {
    const apiReq = req.clone({ url: baseUrl + req.url });
    return next(apiReq);
  }
  return next(req);
};