import { TestBed } from '@angular/core/testing';

import { ContinentsResolver } from './continents.resolver';

describe('ContinentsResolver', () => {
  let resolver: ContinentsResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(ContinentsResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
